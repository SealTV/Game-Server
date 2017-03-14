using System;
using System.IO;
using NUnit.Framework;
using Shared.DataPackages.Client;
using Shared.POCO;

namespace Tests.DataPackages.Client
{
    [TestFixture]
    public class ClientPackageFactoryTests
    {
        private SetTargetsPackage _setTargetsPackage;
        private ClientPackageFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _factory = new ClientPackageFactory();

            _setTargetsPackage = new SetTargetsPackage
            {
                Units = new Unit[5]
            };
            for (int i = 0; i < _setTargetsPackage.Units.Length; i++)
            {
                var unit = new Unit
                {
                    Id = i + 1,
                    Position = new Position(1 * i, 5 * i),
                    State = States.Move,
                    TargetPosition = new Position(1 * i, 5 * i)
                };

                _setTargetsPackage.Units[i] = unit;
            }
        }
        [Test]
        public void GetNextPackageEmptyTest()
        {
            var data = new byte[] {0, 0, 0, 0};

            ClientPackage clientPackage = null;
            using (var stream = new MemoryStream(data))
            {
                Console.WriteLine(stream.Length);
                clientPackage = _factory.GetNextPackage(stream);
            }

            Assert.Null(clientPackage);
        }

        [Test]
        public void GetNextPackageByArrayEmptyTest()
        {
            var data = new byte[] {0, 0, 0, 0};

            ClientPackage clientPackage  = _factory.GetNextPackage(data);
            Assert.Null(clientPackage);
        }

        [Test]
        public void GetPingPackageTest()
        {
            var package = new PingPackage { Value = 10 };
            var data = package.ToByteArray();

            ClientPackage clientPackage = _factory.GetNextPackage(data);

            Assert.NotNull(clientPackage);
            Assert.AreEqual(data, clientPackage.ToByteArray());
            Assert.AreEqual(package.Type, clientPackage.Type);
        }

        [Test]
        public void GetLoginPackageTest()
        {
            var package = new LoginPackage();
            var data = package.ToByteArray();

            ClientPackage clientPackage = _factory.GetNextPackage(data);

            Assert.NotNull(clientPackage);
            Assert.AreEqual(data, clientPackage.ToByteArray());
            Assert.AreEqual(package.Type, clientPackage.Type);
        }

        [Test]
        public void GetRoomPackageTest()
        {
            var package = new GetRoomPackage();
            var data = package.ToByteArray();

            ClientPackage clientPackage = _factory.GetNextPackage(data);

            Assert.NotNull(clientPackage);
            Assert.AreEqual(data, clientPackage.ToByteArray());
            Assert.AreEqual(package.Type, clientPackage.Type);
        }

        [Test]
        public void ExitFromRoomPackageTest()
        {
            var package = new ExitFromRoomPackage
            {
                RoomId = 1
            };
            var data = package.ToByteArray();

            ClientPackage clientPackage = _factory.GetNextPackage(data);

            Assert.NotNull(clientPackage);
            Assert.AreEqual(data, clientPackage.ToByteArray());
            Assert.AreEqual(package.Type, clientPackage.Type);
            Assert.AreEqual(package.RoomId, ((ExitFromRoomPackage)clientPackage).RoomId);
        }

        [Test]
        public void SetTargetsPackageTest()
        {
            var data = _setTargetsPackage.ToByteArray();

            ClientPackage clientPackage = _factory.GetNextPackage(data);

            Assert.NotNull(clientPackage);
            Assert.AreEqual(_setTargetsPackage.Type, clientPackage.Type);
            Assert.AreEqual(_setTargetsPackage.Units.Length, ((SetTargetsPackage)clientPackage).Units.Length);
            Assert.AreEqual(data, clientPackage.ToByteArray());
        }
    }
}
