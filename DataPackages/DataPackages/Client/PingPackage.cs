using System;
using System.IO;

namespace Shared.DataPackages.Client
{
    public sealed class PingPackage : ClientPackage
    {
        public override ClientPackageType Type => ClientPackageType.Ping;

        public int Value { get; set; }

        protected override void ToByteArray(Stream stream)
        {
            stream.Write(BitConverter.GetBytes(Value), 0, 4);
        }

        public override void FromByteArray(byte[] data)
        {
            Value = BitConverter.ToInt32(data, 0);
        }
    }
}
