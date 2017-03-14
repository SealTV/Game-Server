using System;

namespace Shared.POCO
{
    public struct PositionF
    {
        public float X { get; set; }
        public float Y { get; set; }

        public float Magnitude => (float) Math.Sqrt(X * X + Y * Y);

        public PositionF(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static PositionF operator +(PositionF a, PositionF b)
        {
            return new PositionF(a.X + b.X, a.Y + b.Y);
        }

        public static PositionF operator -(PositionF a, PositionF b)
        {
            return new PositionF(a.X - b.X, a.Y - b.Y);
        }

        public static PositionF operator -(PositionF a)
        {
            return new PositionF(-a.X, -a.Y);
        }

        public static PositionF operator *(PositionF a, float d)
        {
            return new PositionF(a.X * d, a.Y * d);
        }

        public static PositionF operator *(float d, PositionF a)
        {
            return new PositionF(a.X * d, a.Y * d);
        }

        public static PositionF operator /(PositionF a, float d)
        {
            return new PositionF(a.X / d, a.Y / d);
        }

        public static bool operator ==(PositionF lhs, PositionF rhs)
        {
            return SqrMagnitude(lhs - rhs) < 9.99999943962493E-11;
        }

        public static bool operator !=(PositionF lhs, PositionF rhs)
        {
            return SqrMagnitude(lhs - rhs) >= 9.99999943962493E-11;
        }

        public static float SqrMagnitude(PositionF a)
        {
            return a.X * a.X + a.Y * a.Y;
        }

        public static PositionF MoveTowards(PositionF current, PositionF target, float maxDistanceDelta)
        {
            PositionF positionF = target - current;
            float magnitude = positionF.Magnitude;
            if ((double)magnitude <= maxDistanceDelta || magnitude == 0.0)
                return target;
            return current + positionF / magnitude * maxDistanceDelta;
        }
    }
}
