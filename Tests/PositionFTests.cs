using NUnit.Framework;
using Shared.POCO;

namespace Tests
{
    [TestFixture]
    public sealed class PositionFFTests
    {
        [Test]
        public void FloatInitTest()
        {
            PositionF p1 = new PositionF(3, 4);
            PositionF p2 = new PositionF(3f, 4f);

            Assert.AreEqual(p1, p2);
        }

        [Test]
        public void MagnitudeTest()
        {
            PositionF p = new PositionF(3, 4);
            var m = p.Magnitude;

            Assert.AreEqual(5, m);
        }

        [Test]
        public void SumTest()
        {
            PositionF p1 = new PositionF(1, 1);
            PositionF p2 = new PositionF(2, 2);
            PositionF p3 = new PositionF(3, 3);

            Assert.AreEqual(p3, p1 + p2);
        }

        [Test]
        public void MinusTest()
        {
            PositionF p1 = new PositionF(1, 1);
            PositionF p2 = new PositionF(2, 2);
            PositionF p3 = new PositionF(3, 3);

            Assert.AreEqual(p1, p3 - p2);
        }

        [Test]
        public void InvertTest()
        {
            PositionF p1 = new PositionF(1, 1);
            PositionF p2 = new PositionF(-1, -1);
            PositionF p3 = new PositionF(3, 3);

            Assert.AreEqual(p1, -p2);
        }

        [Test]
        public void MultiplicationTest()
        {
            PositionF p1 = new PositionF(10, 10);
            PositionF p2 = new PositionF(1, 1);
            float v = 10;

            Assert.AreEqual(p1, p2 * v);
            Assert.AreEqual(p1, v * p2);
        }

        [Test]
        public void DivisionTest()
        {
            PositionF p1 = new PositionF(10, 10);
            PositionF p2 = new PositionF(1, 1);
            float v = 10;

            Assert.AreEqual(p2, p1 / v);
        }

        [Test]
        public void EqualTest()
        {
            PositionF p1 = new PositionF(1, 1);
            PositionF p2 = new PositionF(1, 1);
            PositionF p3 = new PositionF(1, 2);

            Assert.True(p1 == p2);
            Assert.False(p1 == p3);
        }

        [Test]
        public void NotEqualTest()
        {
            PositionF p1 = new PositionF(1, 1);
            PositionF p2 = new PositionF(1, 1);
            PositionF p3 = new PositionF(1, 2);

            Assert.False(p1 != p2);
            Assert.True(p1 != p3);
        }

        [Test]
        public void MoveTowardsTest()
        {
            PositionF p1 = new PositionF(3, 3);
            PositionF p2 = new PositionF(4, 4);
            float maxDistanceDelta = 2;

            PositionF p3 = PositionF.MoveTowards(p1, p2, maxDistanceDelta);
            PositionF p4 = PositionF.MoveTowards(p1, p2, 0);
            PositionF p5 = PositionF.MoveTowards(p2, p2, maxDistanceDelta);

            Assert.AreEqual(p2, p3);
            Assert.AreEqual(p1, p4);
            Assert.AreEqual(p2, p5);
        }
    }
}
