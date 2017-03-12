﻿using System.IO;
using NUnit.Framework;
using Shared.Packages.Client;

namespace Tests.DataPackages.Client
{
    [TestFixture]
    public class GetRoomPackageTests
    {
        [Test]
        public void GetRoomPackageDeserializationTest()
        {
            GetRoomPackage expected = new GetRoomPackage();

            var buffer = expected.ToByteArray();

            ClientPackageType packageType = ClientPackageType.None;
            GetRoomPackage actual = new GetRoomPackage();

            using (var stream = new MemoryStream(buffer))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var len = reader.ReadInt32();
                    packageType = (ClientPackageType)reader.ReadByte();
                    actual.FromByteArray(reader.ReadBytes(len - 1));
                }
            }

            Assert.AreEqual(ClientPackageType.GetRoom, packageType);
        }
    }
}