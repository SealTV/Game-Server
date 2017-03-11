using System;
using System.IO;
using NUnit.Framework;
using Shared.Packages.Client;

namespace Tests.DataPackages.Client
{
    [TestFixture]
    public class ClientPackageFactoryTests
    {
        [Test]
        public void GetNextPackageTest()
        {
            var package = new PingPackage {Value = 10};
            var data = package.ToByteArray();

            ClientPackageFactory factory = new ClientPackageFactory();
            ClientPackage clientPackage = null;
            using (var stream = new MemoryStream(data))
            {
                Console.WriteLine(stream.Length);
                clientPackage = factory.GetNextPackage(stream);
            }

            Assert.NotNull(clientPackage);
            Assert.AreEqual(data, clientPackage.ToByteArray());
        }
    }
}
