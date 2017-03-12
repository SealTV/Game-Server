using System.IO;
using NUnit.Framework;
using Shared.DataPackages.Server;

namespace Tests.DataPackages.Server
{
    [TestFixture]
    internal sealed class PongPackgeTests
    {
        [Test]
        public void PongConstructorTest()
        {
            var packageBase = new PongPackage
            {
                Value =  10
            };

            Assert.AreEqual(10, packageBase.Value);
            Assert.AreEqual(ServerPackageType.Pong, packageBase.Type);
        }

        [Test]
        public void PongSerializationTest()
        {
            var packageBase = new PongPackage
            {
                Value =  10
            };

            var buffer = packageBase.ToByteArray();

            Assert.AreEqual(9, buffer.Length);
        }

        [Test]
        public void PongPackageDeserializationTest()
        {
            PongPackage expected = new PongPackage
            {
                Value = 10
            };

            var buffer = expected.ToByteArray();

            PongPackage actual = new PongPackage();
            int len = 0;
            ServerPackageType packageType = ServerPackageType.None;

            using (var stream = new MemoryStream(buffer))
            {
                using (var reader = new BinaryReader(stream))
                {
                    len = reader.ReadInt32();
                    packageType = (ServerPackageType) reader.ReadByte();
                    actual.FromByteArray(reader.ReadBytes(len - 1));
                }
            }

            Assert.AreEqual(5, len);
            Assert.AreEqual(ServerPackageType.Pong, packageType);
            Assert.AreEqual(9, buffer.Length);
            Assert.AreEqual(expected.Value, actual.Value);
        }
    }
}
