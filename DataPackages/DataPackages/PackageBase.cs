using System.IO;

namespace Shared.DataPackages
{
    public abstract class PackageBase
    {
        public abstract byte[] ToByteArray();
        protected abstract void ToByteArray(Stream stream);
        public abstract void FromByteArray(byte[] data);
    }
}
