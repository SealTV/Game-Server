using System;
using System.IO;

namespace Shared.Packages.Client
{
    public sealed class PingPackage : ClientPackage
    {
        public override ClientPackageType Type => ClientPackageType.Ping;

        public int Value { get; set; }

        protected override void ToByteArray(Stream stream)
        {
            stream.Write(BitConverter.GetBytes(Value), 0, 4);
        }

        public override void FromByteArray(byte[] array)
        {
            Value = BitConverter.ToInt32(array, 0);
        }
    }
}
