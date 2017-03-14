using System.IO;
using NUnit.Framework;
using Shared.DataPackages.Client;
using Shared.POCO;

namespace Tests.DataPackages.Client
{
    [TestFixture]
    public sealed class SetTargetsPackageTests
    {
        private SetTargetsPackage _expected;

        [SetUp]
        public void SetUp()
        {
            _expected = new SetTargetsPackage
            {
                Units = new Unit[5]
            };
            for (int i = 0; i < _expected.Units.Length; i++)
            {
                var unit = new Unit
                {
                    Id = i + 1,
                    Position = new Position(1 * i, 5 * i),
                    State = States.Move,
                    TargetPosition = new Position(1 * i, 5 * i)
                };

                _expected.Units[i] = unit;
            }
        }

        [Test]
        public void ExitFromRoomPackageDeserializationTest()
        {
            var buffer = _expected.ToByteArray();

            SetTargetsPackage actual = new SetTargetsPackage();
            ClientPackageType packageType = ClientPackageType.None;

            using (var stream = new MemoryStream(buffer))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var len = reader.ReadInt32();
                    packageType = (ClientPackageType)reader.ReadByte();
                    actual.FromByteArray(reader.ReadBytes(len - 1));
                }
            }

            Assert.AreEqual(_expected.Type, packageType);
            Assert.AreEqual(_expected.Units.Length, actual.Units.Length);
        }
    }
}
