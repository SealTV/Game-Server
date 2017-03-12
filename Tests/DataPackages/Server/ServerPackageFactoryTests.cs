using System;
using System.IO;
using NUnit.Framework;
using Shared.DataPackages.Client;
using Shared.DataPackages.Server;
using Shared.POCO;

namespace Tests.DataPackages.Server
{
    [TestFixture]
    public class ServerPackageFactoryTests
    {
        private UpdatePositionsPackage _updatePositionsPackage;
        private ServerPackageFactory _factory;
        private PongPackage _pongPackage;
        private AcceptLoginPackage _acceptLoginPackage;
        private SetRoomPackage _setRoomPackage;

     [SetUp]
        public void SetUp()
        {
            _factory = new ServerPackageFactory();
            _pongPackage = new PongPackage {Value = 10};

            _acceptLoginPackage = new AcceptLoginPackage{ClientId = 10};
            _setRoomPackage = new SetRoomPackage
            {
                Room = new Room
                {
                    Id = 1,
                    Width = 10,
                    Height = 10,
                    Units = new[]
                    {
                        new Unit
                        {
                            Id = 1,
                            Position = new Position(),
                            State = States.Move
                        }
                    }
                }
            };

            _updatePositionsPackage = new UpdatePositionsPackage
            {
                Units = new Unit[5]
            };
            for (int i = 0; i < _updatePositionsPackage.Units.Length; i++)
            {
                var unit = new Unit
                {
                    Id = i + 1,
                    Position = new Position {X = 1 * i, Y = 5 * i},
                    State = States.Move,
                    TargetPosition = null
                };

                _updatePositionsPackage.Units[i] = unit;
            }
        }

        [Test]
        public void GetNextPackageEmptyTest()
        {
            var data = new byte[] {0, 0, 0, 0};

            ServePackage package = null;
            using (var stream = new MemoryStream(data))
            {
                Console.WriteLine(stream.Length);
                package = _factory.GetNextPackage(stream);
            }

            Assert.Null(package);
        }

        [Test]
        public void GetNextPackageByArrayEmptyTest()
        {
            var data = new byte[] {0, 0, 0, 0};

            ServePackage package  = _factory.GetNextPackage(data);
            Assert.Null(package);
        }

        [Test]
        public void GetPongPackageTest()
        {
            var data = _pongPackage.ToByteArray();

            ServePackage package = _factory.GetNextPackage(data);

            Assert.NotNull(package);
            Assert.AreEqual(data, package.ToByteArray());
            Assert.AreEqual(_pongPackage.Type, package.Type);
        }

        [Test]
        public void GetAcceptLoginPackageTest()
        {
            var data = _acceptLoginPackage.ToByteArray();

            ServePackage package = _factory.GetNextPackage(data);

            Assert.NotNull(package);
            Assert.AreEqual(data, package.ToByteArray());
            Assert.AreEqual(_acceptLoginPackage.Type, package.Type);
            Assert.AreEqual(_acceptLoginPackage.ClientId, ((AcceptLoginPackage)package).ClientId);
        }

        [Test]
        public void GetSetRoomPackageTest()
        {
            var data = _setRoomPackage.ToByteArray();

            ServePackage clientPackage = _factory.GetNextPackage(data);

            Assert.NotNull(clientPackage);
            Assert.AreEqual(data, clientPackage.ToByteArray());
            Assert.AreEqual(_setRoomPackage.Type, clientPackage.Type);
        }

       

        [Test]
        public void UpdatePositionsPackageTest()
        {
            var data = _updatePositionsPackage.ToByteArray();

            ServePackage package = _factory.GetNextPackage(data);

            Assert.NotNull(package);
            Assert.AreEqual(_updatePositionsPackage.Type, package.Type);
            Assert.AreEqual(_updatePositionsPackage.Units.Length, ((UpdatePositionsPackage)package).Units.Length);
            Assert.AreEqual(data, package.ToByteArray());
        }
    }
}
