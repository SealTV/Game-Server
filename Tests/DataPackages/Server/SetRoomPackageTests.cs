using System.IO;
using NUnit.Framework;
using Shared.DataPackages.Server;
using Shared.POCO;

namespace Tests.DataPackages.Server
{
    [TestFixture]
    public class SetRoomPackageTests
    {
        private SetRoomPackage _expected;

        [SetUp]
        public void SetUp()
        {
            _expected = new SetRoomPackage
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
        }

        [Test]
        public void SetRoomPackageDeserializationTest()
        {
            var buffer = _expected.ToByteArray();

            SetRoomPackage actual = new SetRoomPackage();
            ServerPackageType packageType = ServerPackageType.None;

            using (var stream = new MemoryStream(buffer))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var len = reader.ReadInt32();
                    packageType = (ServerPackageType)reader.ReadByte();
                    actual.FromByteArray(reader.ReadBytes(len - 1));
                }
            }

            Assert.AreEqual(_expected.Type, packageType);
            Assert.AreEqual(_expected.Room.Id, actual.Room.Id);
            Assert.AreEqual(_expected.Room.Units.Length, actual.Room.Units.Length);
        }
    }
}
