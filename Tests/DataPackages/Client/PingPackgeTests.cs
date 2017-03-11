using System.IO;
using NUnit.Framework;
using Shared.Packages.Client;

namespace Tests.DataPackages.Client
{
    [TestFixture]
    internal sealed class PingPackgeTests
    {
        [Test]
        public void PingConstructorTest()
        {
            var packageBase = new PingPackage
            {
                Value =  10
            };

            Assert.AreEqual(10, packageBase.Value);
            Assert.AreEqual(ClientPackageType.Ping, packageBase.Type);
        }

        [Test]
        public void PingSerializationTest()
        {
            var packageBase = new PingPackage
            {
                Value =  10
            };

            var buffer = packageBase.ToByteArray();

            Assert.AreEqual(9, buffer.Length);
        }

        [Test]
        public void PingPackageDeserializationTest()
        {
            PingPackage expected = new PingPackage
            {
                Value = 10
            };

            var buffer = expected.ToByteArray();

            PingPackage actual = new PingPackage();
            int len = 0;
            ClientPackageType packageType = ClientPackageType.None;

            using (var stream = new MemoryStream(buffer))
            {
                using (var reader = new BinaryReader(stream))
                {
                    len = reader.ReadInt32();
                    packageType = (ClientPackageType) reader.ReadByte();
                    actual.FromByteArray(reader.ReadBytes(len - 1));
                }
            }

            Assert.AreEqual(5, len);
            Assert.AreEqual(ClientPackageType.Ping, packageType);
            Assert.AreEqual(9, buffer.Length);
            Assert.AreEqual(expected.Value, actual.Value);
        }
    }
}
