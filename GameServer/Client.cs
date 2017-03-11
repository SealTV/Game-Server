using System.Linq;
using System.Net.Sockets;
using Shared.Packages.Client;

namespace GameServer
{
    public class Client
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static int ClientCount = 0;

        private readonly TcpClient _tcpClient;
        private readonly NetworkStream _stream;
        private readonly ClientPackageFactory _factory;

        public Client(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            _stream = _tcpClient.GetStream();
            _factory = new ClientPackageFactory();
            ClientCount++;
        }

        public async void Process()
        {
            byte[] data = new byte[1024];

            do
            {
                int len = await _stream.ReadAsync(data, 0, data.Length);
                var package = _factory.GetNextPackage(data.Take(len).ToArray());

                if (package != null)
                {
                    Logger.Debug($"Client connected {_tcpClient.Client.RemoteEndPoint}");
                    Logger.Debug(package.Type);
                    Logger.Debug(((PingPackage) package).Value);
                    Logger.Debug($"Clients count {ClientCount}");
                }
                else
                {
                    ClientCount--;
                    Logger.Debug(ClientCount);

                    return;
                }

            } while (_tcpClient.Connected);
        }

    }
}