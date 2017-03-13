using System;
using System.IO;

namespace Shared.DataPackages.Server
{
   public sealed class AcceptLoginPackage : ServerPackage
    {
        public override ServerPackageType Type => ServerPackageType.AcceptLogin;

        public int ClientId { get; set; }

        protected override void ToByteArray(Stream stream)
        {
            stream.Write(BitConverter.GetBytes(ClientId), 0, 4);
        }

        public override void FromByteArray(byte[] data)
        {
            ClientId = BitConverter.ToInt32(data, 0);
        }
    }
}
