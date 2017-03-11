using System;

namespace GameServer
{
    public sealed class Program
    {
        private static void Main(string[] args)
        {
            Server  server = new Server("127.0.0.1", 3990);
            server.StartAsync();
            Console.ReadKey();
        }
    }
}
