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
            server.Stop();
//            Random random = new Random();
//            int n = random.Next(7, 13);
//            Room room = new Room
//            {
//                Width = n,
//                Height = n,
////                Units = new Unit[random.Next(1, 5)]
//                Units = new Unit[1]
//            };
//
//            List<Position> startPositions = new List<Position>();
//            while (startPositions.Count < room.Units.Length)
//            {
//                var position = new Position(random.Next(0, n), random.Next(0, n));
//                if (startPositions.Any(p => p.X == position.X && p.Y == position.Y))
//                    continue;
//
//                startPositions.Add(position);
//            }
//
//            List<Position> targets = new List<Position>();
//            while (targets.Count < room.Units.Length)
//            {
//                var position = new Position(random.Next(0, n), random.Next(0, n));
//                if (startPositions.Any(p => p.X == position.X && p.Y == position.Y))
//                    continue;
//
//                targets.Add(position);
//            }
//
//            for (int i = 0; i < room.Units.Length; i++)
//            {
//                room.Units[i] = new Unit
//                {
//                    Id = i + 1,
//                    Position = startPositions[i],
//                    State = States.Stay,
//                    TargetPosition = targets[i]
//                };
//
//            }
//
//            Logger.Debug($"start {startPositions[0].X} : {startPositions[0].Y} target {targets[0].X}:{targets[0].Y}");
//            Console.ReadKey();
//            Game.Game game  = new Game.Game(room);
//            game.Start();
//            Console.ReadKey();
//            game.Stop();
        }
    }
}
