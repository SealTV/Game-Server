using System.IO;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Shared.Packages.Client;
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
                    Position = new Position { X = 1 * i, Y = 5 * i},
                    State =  States.Move,
                    TargetPosition = null
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
