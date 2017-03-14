using NUnit.Framework;
using Shared.POCO;

namespace Tests
{
    [TestFixture]
    public sealed class PositionTests
    {
        [Test]
        public void FloatInitTest()
        {
            Position p1 = new Position(3, 4);
            Position p2 = new Position(3f, 4f);

            Assert.AreEqual(p1, p2);
        }

        [Test]
        public void MagnitudeTest()
        {
            Position p = new Position(3, 4);
            var m = p.Magnitude;

            Assert.AreEqual(5, m);
        }

        [Test]
        public void SumTest()
        {
            Position p1 = new Position(1, 1);
            Position p2 = new Position(2, 2);
            Position p3 = new Position(3, 3);

            Assert.AreEqual(p3, p1 + p2);
        }

        [Test]
        public void MinusTest()
        {
            Position p1 = new Position(1, 1);
            Position p2 = new Position(2, 2);
            Position p3 = new Position(3, 3);

            Assert.AreEqual(p1, p3 - p2);
        }

        [Test]
        public void InvertTest()
        {
            Position p1 = new Position(1, 1);
            Position p2 = new Position(-1, -1);
            Position p3 = new Position(3, 3);

            Assert.AreEqual(p1, -p2);
        }

        [Test]
        public void MultiplicationTest()
        {
            Position p1 = new Position(10, 10);
            Position p2 = new Position(1, 1);
            float v = 10;

            Assert.AreEqual(p1, p2 * v);
            Assert.AreEqual(p1, v * p2);
        }

        [Test]
        public void DivisionTest()
        {
            Position p1 = new Position(10, 10);
            Position p2 = new Position(1, 1);
            float v = 10;

            Assert.AreEqual(p2, p1 / v);
        }

        [Test]
        public void EqualTest()
        {
            Position p1 = new Position(1, 1);
            Position p2 = new Position(1, 1);
            Position p3 = new Position(1, 2);

            Assert.True(p1 == p2);
            Assert.False(p1 == p3);
        }

        [Test]
        public void NotEqualTest()
        {
            Position p1 = new Position(1, 1);
            Position p2 = new Position(1, 1);
            Position p3 = new Position(1, 2);

            Assert.False(p1 != p2);
            Assert.True(p1 != p3);
        }

        [Test]
        public void MoveTowardsTest()
        {
            Position p1 = new Position(3, 3);
            Position p2 = new Position(4, 4);
            float maxDistanceDelta = 2;

            Position p3 = Position.MoveTowards(p1, p2, maxDistanceDelta);
            Position p4 = Position.MoveTowards(p1, p2, 0);
            Position p5 = Position.MoveTowards(p2, p2, maxDistanceDelta);

            Assert.AreEqual(p2, p3);
            Assert.AreEqual(p1, p4);
            Assert.AreEqual(p2, p5);
        }
    }
}
