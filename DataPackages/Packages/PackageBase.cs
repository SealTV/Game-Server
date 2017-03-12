using System.IO;

namespace Shared.Packages
{
    public abstract class PackageBase
    {
        public abstract byte[] ToByteArray();
        protected abstract void ToByteArray(Stream stream);
        public abstract void FromByteArray(byte[] array);
    }
}
