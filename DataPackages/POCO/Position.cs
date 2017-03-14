using System;

namespace Shared.POCO
{
    public struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public float Magnitude => (float) Math.Sqrt(X * X + Y * Y);

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position(float x, float y)
        {
            X = (int)x;
            Y = (int)y;
        }

        public static Position operator +(Position a, Position b)
        {
            return new Position(a.X + b.X, a.Y + b.Y);
        }

        public static Position operator -(Position a, Position b)
        {
            return new Position(a.X - b.X, a.Y - b.Y);
        }

        public static Position operator -(Position a)
        {
            return new Position(-a.X, -a.Y);
        }

        public static Position operator *(Position a, float d)
        {
            return new Position(a.X * d, a.Y * d);
        }

        public static Position operator *(float d, Position a)
        {
            return new Position(a.X * d, a.Y * d);
        }

        public static Position operator /(Position a, float d)
        {
            return new Position(a.X / d, a.Y / d);
        }

        public static bool operator ==(Position lhs, Position rhs)
        {
            return SqrMagnitude(lhs - rhs) < 9.99999943962493E-11;
        }

        public static bool operator !=(Position lhs, Position rhs)
        {
            return SqrMagnitude(lhs - rhs) >= 9.99999943962493E-11;
        }

        public static float SqrMagnitude(Position a)
        {
            return a.X * a.X + a.Y * a.Y;
        }

        public static Position MoveTowards(Position current, Position target, float maxDistanceDelta)
        {
            Position position = target - current;
            float magnitude = position.Magnitude;
            if (magnitude <= maxDistanceDelta || magnitude == 0.0)
                return target;
            return current + position / magnitude * maxDistanceDelta;
        }
    }
}
