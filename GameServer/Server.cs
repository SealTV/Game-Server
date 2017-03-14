using System;
using System.Collections.Generic;
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

        private CancellationTokenSource _tokenSource;
        private List<CancellationTokenSource> _tokenSources;
 
        public Server(string host, int port)
        {
            _tokenSource = new CancellationTokenSource();
            _tokenSources = new List<CancellationTokenSource>();
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

            _listener.Start();
            Logger.Warn("Server started!");

            while (!_tokenSource.IsCancellationRequested)
            {
                var tcpClient = await _listener.AcceptTcpClientAsync();
                Logger.Debug($"Client connected {tcpClient.Client.RemoteEndPoint}");

                CancellationTokenSource tokenSource = new CancellationTokenSource();

                var client = new Client(tcpClient, tokenSource);

                _tokenSources.Add(tokenSource);
                client.Process();
            }
        }

        public void Stop()
        {
            _tokenSource.Cancel();
            _listener.Stop();
            foreach (var source in _tokenSources)
            {
                if(!_tokenSource.IsCancellationRequested)
                    source.Cancel();
            }
        }
    }
}
