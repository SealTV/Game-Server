using System.IO;

namespace DataPackages.Client
{
    public abstract class ClientPackage : IPackage
    {
        public abstract ClientPackageType Type { get; }

        public abstract void ToByteArray(Stream stream);
        public abstract void FromByteArray(byte[] array);
    }
}
