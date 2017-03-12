using System.IO;
using NUnit.Framework;
using Shared.Packages.Client;

namespace Tests.DataPackages.Client
{
    [TestFixture]
    public sealed class LoginPackageTests
    {
        [Test]
        public void LoginPackageDeserializationTest()
        {
            LoginPackage expected = new LoginPackage();

            var buffer = expected.ToByteArray();

            ClientPackageType packageType = ClientPackageType.None;
            LoginPackage actual = new LoginPackage();

            using (var stream = new MemoryStream(buffer))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var len = reader.ReadInt32();
                    packageType = (ClientPackageType)reader.ReadByte();
                    actual.FromByteArray(reader.ReadBytes(len - 1));
                }
            }

            Assert.AreEqual(ClientPackageType.Login, packageType);
        }
    }
}
