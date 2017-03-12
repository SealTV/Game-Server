using System.IO;
using NUnit.Framework;
using Shared.Packages.Client;

namespace Tests.DataPackages.Client
{
    [TestFixture]
    public class ExitFromRoomPackageTests
    {
        [Test]
        public void ExitFromRoomPackageDeserializationTest()
        {
            ExitFromRoomPackage expected = new ExitFromRoomPackage {RoomId = 1};

            var buffer = expected.ToByteArray();

            ExitFromRoomPackage actual = new ExitFromRoomPackage();
            actual.FromByteArray(buffer);

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

            Assert.AreEqual(expected.Type, packageType);
            Assert.AreEqual(expected.RoomId, actual.RoomId);
        }
    }
}
