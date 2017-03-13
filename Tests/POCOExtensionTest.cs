using NUnit.Framework;
using Shared;
using Shared.POCO;

namespace Tests
{
    [TestFixture]
    public class POCOExtensionTest
    {
        [Test]
        public void PositionSerializationTest()
        {
            var data = new byte[] {0, 0, 0, 0, 0, 0, 0, 0};
            Position p = new Position {X = 0, Y = 0};

            var array = p.ToByteArray();

            Assert.AreEqual(data.Length, array.Length);
            Assert.AreEqual(data, array);
        }

        [Test]
        public void PositionDeserializationTest()
        {
            Position p1 = new Position {X = 10, Y = 125};

            var array = p1.ToByteArray();

            var p2 = new Position();
            p2.FromBytes(array);

            Assert.AreEqual(p1.X, p2.X);
            Assert.AreEqual(p1.Y, p2.Y);
        }

        [Test]
        public void UnitSerializationTest1()
        {
            var data = new byte[]
            {
                1, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0,
                0, 0, 0, 0, 0, 0, 0, 0};

            Unit u = new Unit
            {
                Id =  1,
                Position = new Position { X = 0, Y = 0 },
                State =  States.Stay,
                TargetPosition = new Position { X = 0, Y = 0}
            };

            var array = u.ToByteArray();

            Assert.AreEqual(data.Length, array.Length);
            Assert.AreEqual(data, array);
        }

        [Test]
        public void UnitSerializationTest2()
        {
            var data = new byte[]
            {
                1, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                1};

            Unit u = new Unit
            {
                Id =  1,
                Position = new Position { X = 0, Y = 0 },
                State =  States.Move,
                TargetPosition = null
            };

            var array = u.ToByteArray();

            Assert.AreEqual(data.Length, array.Length);
            Assert.AreEqual(data, array);
        }

        [Test]
        public void UnitDeserializationTest1()
        {
            Unit unit1 = new Unit
            {
                Id =  1,
                Position = new Position { X = 0, Y = 0 },
                State =  States.Stay,
                TargetPosition = new Position { X = 0, Y = 0}
            };

            var data = unit1.ToByteArray();
            Unit unit2 = new Unit();
            unit2.FromBytes(data);

            Assert.AreEqual(unit1.Id, unit2.Id);
            Assert.AreEqual(unit1.State, unit2.State);
            Assert.AreEqual(unit1.Position.X, unit2.Position.X);
            Assert.AreEqual(unit1.Position.Y, unit2.Position.Y);
            Assert.NotNull(unit2.TargetPosition);
            Assert.AreEqual(unit1.TargetPosition.X, unit2.TargetPosition.X);
            Assert.AreEqual(unit1.TargetPosition.Y, unit2.TargetPosition.Y);
        }

        [Test]
        public void UnitDeserializationTest2()
        {
            Unit unit1 = new Unit
            {
                Id =  1,
                Position = new Position { X = 0, Y = 0 },
                State =  States.Move,
                TargetPosition = null
            };


            var data = unit1.ToByteArray();
            Unit unit2 = new Unit();
            unit2.FromBytes(data);

            Assert.AreEqual(unit1.Id, unit2.Id);
            Assert.AreEqual(unit1.State, unit2.State);
            Assert.AreEqual(unit1.Position.X, unit2.Position.X);
            Assert.AreEqual(unit1.Position.Y, unit2.Position.Y);
            Assert.Null(unit2.TargetPosition);
        }

        [Test]
        public void UnitDeserializationTest3()
        {
            Unit unit1 = new Unit
            {
                Id = 1,
                Position = new Position { X = 10, Y = 150 },
                State = States.Stay,
                TargetPosition = new Position { X = 13, Y = 26 }
            };

            var data = unit1.ToByteArray();
            Unit unit2 = new Unit();
            unit2.FromBytes(data);

            Assert.AreEqual(unit1.Id, unit2.Id);
            Assert.AreEqual(unit1.State, unit2.State);
            Assert.AreEqual(unit1.Position.X, unit2.Position.X);
            Assert.AreEqual(unit1.Position.Y, unit2.Position.Y);
            Assert.NotNull(unit2.TargetPosition);
            Assert.AreEqual(unit1.TargetPosition.X, unit2.TargetPosition.X);
            Assert.AreEqual(unit1.TargetPosition.Y, unit2.TargetPosition.Y);
        }

        [Test]
        public void UnitArraySerializationTest()
        {
            Unit unit = new Unit
            {
                Id = 1,
                Position = new Position { X = 10, Y = 150 },
                State = States.Stay,
                TargetPosition = new Position { X = 13, Y = 26 }
            };

            Unit[] array = { unit , unit };

            var data = array.ToByteArray();

            Assert.AreEqual(54, data.Length);
        }

        [Test]
        public void UnitArrayDeserializationTest()
        {
            Unit unit = new Unit
            {
                Id = 1,
                Position = new Position { X = 10, Y = 150 },
                State = States.Stay,
                TargetPosition = new Position { X = 13, Y = 26 }
            };

            Unit[] array = { unit , unit };

            var data = array.ToByteArray();


            Unit[] array2 = new Unit[0];
            array2 = array2.FromBytes(data);

            Assert.AreEqual(array.Length, array2.Length);
        }

        [Test]
        public void RoomDeserializationTest()
        {
            Unit unit = new Unit
            {
                Id = 1,
                Position = new Position { X = 10, Y = 150 },
                State = States.Stay,
                TargetPosition = new Position { X = 13, Y = 26 }
            };

            Room origin = new Room
            {
                Id = 1,
                Width = 10,
                Height = 10,
                Units = new[] {unit, unit}
            };

            var data = origin.ToByteArray();

            Room target = new Room();
            target.FromBytes(data);

            Assert.AreEqual(origin.Id, target.Id);
            Assert.AreEqual(origin.Width, target.Width);
            Assert.AreEqual(origin.Height, target.Height);
            Assert.AreEqual(origin.Units.Length, target.Units.Length);
        }
    }
}
