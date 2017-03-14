using System;
using System.Collections.Generic;
using System.Linq;
using Shared.DataPackages.Client;
using Shared.DataPackages.Server;
using Shared.POCO;

namespace GameServer.PackageHandlers
{
    public sealed class GetRoomPackageHandler : PackageHandler
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public GetRoomPackageHandler(Client client, ClientPackage package) : base(client, package)
        {
        }

        public override void HandlePackage()
        {
            Logger.Debug("Handle GetRoom package");

            Random random = new Random();
            int n = random.Next(7, 13);
            Room room = new Room
            {
                Width = n,
                Height = n,
                Units = new Unit[random.Next(1, 5)]
            };

            List<Position> startPositions = new List<Position>();
            while (startPositions.Count < room.Units.Length)
            {
                var position = new Position(random.Next(0, n), random.Next(0, n));
                if(startPositions.Any(p => p.X == position.X && p.Y == position.Y))
                    continue;
                
                startPositions.Add(position);
            }

            for (int i = 0; i < room.Units.Length; i++)
            {
                room.Units[i] = new Unit
                {
                    Id =  i + 1,
                    Position = startPositions[i],
                    State = States.Stay,
                    TargetPosition = startPositions[i]
                };
            }

            Client.SendPackage(new SetRoomPackage
            {
                Room = room
            });

            Game.Game game = new Game.Game(room);
            Client.SetGame(game);
            game.Start();
        }
    }
}