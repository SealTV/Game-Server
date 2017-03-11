using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace GameServer
{
    public class Server
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private bool _isRun;
        private readonly TcpListener _listener;
        private readonly IPEndPoint _ipEndPoint;

        private ManualResetEvent _tokenSource;

        public Server(string host, int port)
        {
            _ipEndPoint = new IPEndPoint(IPAddress.Parse(host), port);
            _isRun = false;
            _listener = new TcpListener(_ipEndPoint);
        }

        public async void StartAsync()
        {
            if (_isRun)
            {
                Logger.Warn("Server already running!");
                return;
            }

            _isRun = true;
            _listener.Start();
            Logger.Warn("Server started!");

            while (_isRun)
            {
                var tcpClient = await _listener.AcceptTcpClientAsync();
                Logger.Debug($"Client connected {tcpClient.Client.RemoteEndPoint}");
                var client = new Client(tcpClient);
                client.Process();
            }
        }
    }
}
