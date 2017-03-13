using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
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

        public Client(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            _stream = _tcpClient.GetStream();
            _packageFactory = new ClientPackageFactory();
            ClientsCount++;
            _tokenSource = new CancellationTokenSource();
        }

        public async void Process()
        {
            byte[] data = new byte[1024];

            do
            {
                int len = await _stream.ReadAsync(data, 0, data.Length, _tokenSource.Token);
                Logger.Debug($"readed {len}");

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

        public async void SendPackage(ServerPackage package)
        {
            var data = package.ToByteArray();
            await _stream.WriteAsync(data, 0, data.Length, _tokenSource.Token);
        }

        public void Close()
        {
            _tokenSource.Cancel();
            _tcpClient.Close();
        }
    }
}