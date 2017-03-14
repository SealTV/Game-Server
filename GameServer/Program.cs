using System;
using System.Collections.Generic;
using System.Linq;
using Shared.POCO;

namespace GameServer
{
    public sealed class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            Server  server = new Server("127.0.0.1", 3990);
            server.StartAsync();
            Console.ReadKey();
            server.Stop();;
        }
    }
}
