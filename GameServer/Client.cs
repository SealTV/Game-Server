using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using GameServer.PackageHandlers;
using Shared.DataPackages.Client;
using Shared.DataPackages.Server;

namespace GameServer
{
    public class Client
    {
        public static int ClientsCount;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly TcpClient _tcpClient;
        private readonly NetworkStream _stream;
        private readonly ClientPackageFactory _packageFactory;

        private readonly CancellationTokenSource _tokenSource;

        private Game.Game _game;

        public Game.Game Game
        {
            get
            {
                return _game;
            }
        }

        public Client(TcpClient tcpClient, CancellationTokenSource tokenSource)
        {
            _game = null;
            _tcpClient = tcpClient;
            _tokenSource = tokenSource;
            _stream = _tcpClient.GetStream();
            _packageFactory = new ClientPackageFactory();
            ClientsCount++;
        }

        public void SetGame(Game.Game game)
        {
            _game = game;
            _game.OnUpdatePositions += units =>
            {
                SendPackage(new UpdatePositionsPackage
                {
                    Units = units
                });
            };
        }

        public async void Process()
        {
            byte[] data = new byte[1024];

            try
            {
                do
                {
                    int len = await _stream.ReadAsync(data, 0, data.Length, _tokenSource.Token);
                    byte[] buffer = new byte[len];
                    Array.Copy(data, buffer, len);
                    using (var stream = new MemoryStream(buffer))
                    {
                        ClientPackage package = null;
                        do
                        {
                            package = _packageFactory.GetNextPackage(stream);
                            if (package != null)
                            {
                                var handler = PackageHandler.GetPackageHandler(this, package);
                                if (handler != null)
                                {
                                    handler.HandlePackage();
                                }
                                else
                                {
                                    Logger.Warn($"Package handler for packafe {package.Type} are not found!");
                                }
                            }
                        } while (package != null);
                    }

                } while (_tcpClient.Connected && !_tokenSource.IsCancellationRequested);
            }
            catch (IOException)
            {
                Close();
            }
            catch (TaskCanceledException)
            {
                Close();
            }
        }

        public async void SendPackage(ServerPackage package)
        {
            var data = package.ToByteArray();
            try
            {
                await _stream.WriteAsync(data, 0, data.Length, _tokenSource.Token);
            }
            catch (IOException)
            {
                Close();
            }
            catch (TaskCanceledException)
            {
                Close();
            }
        }

        public void Close()
        {
            _tokenSource.Cancel();
            _tcpClient.Close();
            _game?.Stop();
        }
    }
}