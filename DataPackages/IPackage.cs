using System.IO;

namespace DataPackages
{
    public interface IPackage
    {
        void ToByteArray(Stream stream);
        void FromByteArray(byte[] array);
    }
}
