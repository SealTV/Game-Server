using System;
using System.IO;
using NUnit.Framework;
using DataPackages.Client;
namespace Tests
{
    [TestFixture]
    internal sealed class Program
    {
        [Test]
        public void FirstTest()
        {
            PingPackage package = new PingPackage
            {
                Value =  10
            };

            Assert.AreEqual(10, package.Value);
            Assert.AreEqual(ClientPackageType.Ping, package.Type);
        }

        [Test]
        public void SecondTest()
        {
            PingPackage package = new PingPackage
            {
                Value =  10
            };

            byte[] buffer = null;
            using (var stream = new MemoryStream())
            {
                package.ToByteArray(stream);
                buffer = stream.ToArray();
            }

            Assert.AreEqual(9, buffer.Length);
        }

        [Test]
        public void ThirdTest()
        {
            PingPackage expected = new PingPackage
            {
                Value = 10
            };

            byte[] buffer = null;
            using (var stream = new MemoryStream())
            {
                expected.ToByteArray(stream);
                buffer = stream.ToArray();
            }

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
        }

        [Test]
        public void FourTest()
        {
            PingPackage p1 = new PingPackage
            {
                Value = 1
            };
            PingPackage p2 = new PingPackage
            {
                Value = 2
            };

            byte[] buffer = null;
            using (var stream = new MemoryStream())
            {
                p1.ToByteArray(stream);
                p2.ToByteArray(stream);
                buffer = stream.ToArray();
            }

            PingPackage actual1 = new PingPackage();
            PingPackage actual2 = new PingPackage();
            using (var stream = new MemoryStream(buffer))
            {
                using (var reader = new BinaryReader(stream))
                {
                    int len = reader.ReadInt32();
                    ClientPackageType packageType = (ClientPackageType) reader.ReadByte();
                    actual1.FromByteArray(reader.ReadBytes(len - 1));
                    len = reader.ReadInt32();
                    packageType = (ClientPackageType) reader.ReadByte();
                    actual2.FromByteArray(reader.ReadBytes(len - 1));
                }
            }

            Assert.AreEqual(p1.Value, actual1.Value);
            Assert.AreEqual(p2.Value, actual2.Value);
        }
    }
}
