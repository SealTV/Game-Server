using System.Collections.Generic;
using Shared.POCO;

namespace GameServer.Game
{
    public sealed class GameUnit
    {
        private const float Speed = 20;

        private PositionF _targetPosition;
        public PositionF Position => Unit.PositionF;
        public PositionF Target => _targetPosition;
        public Position OriginTarget { get; set; }

        public Unit Unit { get; }

        public List<Position> Path { get; set; }

        public States State
        {
            get { return Unit.State; }
            private set { Unit.State = value; }
        }

        public GameUnit(Unit unit)
        {
            Unit = unit;
            Unit.PositionF = new PositionF(unit.Position.X * 10, unit.Position.Y * 10);
            _targetPosition = new PositionF(unit.TargetPosition.X * 10, unit.TargetPosition.Y * 10);
        }

        public void SetTarget(PositionF target)
        {
            _targetPosition = target;
            State = States.Move;
        }

        public void Update(float deltaTime)
        {
            if (Unit.PositionF == _targetPosition && Unit.Position == Unit.TargetPosition)
            {
                State = States.Stay;
                Path = null;
            }

            if (State == States.Stay)
                return;

            Unit.PositionF = PositionF.MoveTowards(Unit.PositionF, _targetPosition, Speed * deltaTime);
        }
    }
}
