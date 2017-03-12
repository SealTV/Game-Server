using System.IO;
using NUnit.Framework;
using Shared.DataPackages.Server;

namespace Tests.DataPackages.Server
{
    [TestFixture]
    public sealed class AcceptLoginPackageTests
    {
        private AcceptLoginPackage _expected;

        [SetUp]
        public void SetUp()
        {
            _expected = new AcceptLoginPackage
            {
                ClientId = 5
            };
        }

        [Test]
        public void AcceptLoginPackageDeserializationTest()
        {
            var buffer = _expected.ToByteArray();

            AcceptLoginPackage actual = new AcceptLoginPackage();
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
            Assert.AreEqual(_expected.ClientId, actual.ClientId);
        }
    }
}
