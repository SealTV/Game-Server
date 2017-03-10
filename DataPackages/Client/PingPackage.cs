using System;
using System.IO;

namespace DataPackages.Client
{
    public sealed class PingPackage : ClientPackage
    {
        public override ClientPackageType Type => ClientPackageType.Ping;
        public int Value;

        public override void ToByteArray(Stream stream)
        {
            using (var str = new MemoryStream())
            {
                using (var writer = new BinaryWriter(str))
                {
                    writer.Write((byte) Type);
                    writer.Write(Value);
                }

                var arr = str.ToArray();
                stream.Write(BitConverter.GetBytes(arr.Length), 0, 4);
                stream.Write(arr, 0, arr.Length);
            }
        }

        public override void FromByteArray(byte[] array)
        {
            using (var stream = new MemoryStream(array))
            using (var reader = new BinaryReader(stream))
            {
                Value = reader.Read();
            }
        }
    }
}
