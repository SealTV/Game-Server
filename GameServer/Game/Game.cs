using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shared.Algorithms;
using Shared.POCO;

namespace GameServer.Game
{
    public sealed class Game
    {
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private const int CellSize = 10;
        private const int FramesPerSecond = 25;
        private const long SkipTicks = 1000 / FramesPerSecond;
        private const int MaxFrameskip = 5;

        private readonly Room _room;
        private readonly int[,] _map;
        
        private readonly ConcurrentDictionary<int, GameUnit> _units;

        private CancellationTokenSource _tokenSource;

        public event Action<Unit[]> OnUpdatePositions = delegate{};
        private int _clientId;

        public Game(Room room, int clientId)
        {
            _clientId = clientId;
            _room = room;
            _map = new int[room.Width, room.Height];
           
            _units = new ConcurrentDictionary<int, GameUnit>();
            foreach (var unit in _room.Units)
            {
                _units.TryAdd(unit.Id, new GameUnit(unit));
            }

            UpdateMap();
        }

        public void Start()
        {
            Logger.Info($"Start game for client {_clientId}");
            _tokenSource = new CancellationTokenSource();
            Task.Run(() => Run(_tokenSource.Token), _tokenSource.Token);
        }

        public void Stop()
        {
            Logger.Info($"Stop game for client {_clientId}");
            _tokenSource.Cancel();
        }

        private void Run(CancellationToken token)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            long nextGameTick = stopwatch.ElapsedMilliseconds;
            int loops;
            while (!token.IsCancellationRequested)
            {
                loops = 0;
                var e = stopwatch.ElapsedMilliseconds;
                while (e > nextGameTick && loops < MaxFrameskip)
                {

                    nextGameTick += SkipTicks;
                    loops++;
                }

                var interpolation = (stopwatch.ElapsedMilliseconds + SkipTicks - nextGameTick) / (float)SkipTicks;

                if (loops > 0)
                {

                    UpdateGame(interpolation);
                    OnUpdatePositions(_units.Values.Select(u => u.Unit).ToArray());
                }
            }

            stopwatch.Stop();
        }

        private void UpdateGame(float deltaTime)
        {
            foreach (var unit in _units.Values)
            {
                unit.Update(deltaTime);
            }

            UpdateMap();

            foreach (var unit in _units.Values)
            {
                if (unit.Unit.Position == unit.Unit.TargetPosition || unit.Position != unit.Target)
                    continue;
                
                if (unit.Path == null || unit.Path.Any(p => p != unit.Unit.Position && _map[p.X, p.Y] != 0))
                {
                    var path = AStarPathSearch.FindPath(_map, unit.Unit.Position, unit.Unit.TargetPosition);
                    if (path != null)
                    {
                        unit.Path = path.GetRange(1, path.Count - 1);
                    }
                    else
                    {
                        var pathToNearestPoint = AStarPathSearch.FindPathToNearestPoint(_map, unit.Unit.Position,
                            unit.OriginTarget);
                        if (pathToNearestPoint.Count == 1 && pathToNearestPoint[0] == unit.Unit.Position)
                        {
                            unit.Unit.TargetPosition = unit.Unit.Position;
                            continue;
                        }
                        unit.Path = pathToNearestPoint.GetRange(1, pathToNearestPoint.Count - 1);
                        unit.Unit.TargetPosition = unit.Path[unit.Path.Count - 1];
                    }
                }

                if(unit.Path.Count == 0)
                    continue;

                _map[unit.Unit.Position.X, unit.Unit.Position.Y] = 0;
                unit.Unit.Position = unit.Path.First(p => p != unit.Unit.Position);
                _map[unit.Unit.Position.X, unit.Unit.Position.Y] = 1;

                unit.Path.Remove(unit.Unit.Position);
                unit.SetTarget(new PositionF(unit.Unit.Position.X * CellSize, unit.Unit.Position.Y * CellSize));
            }

        }

        public void SetTargets(Unit[] units)
        {
            foreach (var unit in units)
            {
                _units[unit.Id].OriginTarget = unit.TargetPosition;
                _units[unit.Id].Unit.TargetPosition = unit.TargetPosition;
                _units[unit.Id].Path = null;
            }
        }

        private void UpdateMap()
        {
            for (int i = 0; i < _room.Width; i++)
            {
                for (int j = 0; j < _room.Height; j++)
                {
                    _map[i, j] = 0;
                }
            }

            foreach (var unit in _units.Values)
            {
                _map[unit.Unit.Position.X, unit.Unit.Position.Y] = 1;
            }
        }
    }
}
