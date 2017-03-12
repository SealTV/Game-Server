using System;
using System.IO;

namespace Shared.Packages.Client
{
    public abstract class ClientPackage : PackageBase
    {
        public abstract ClientPackageType Type { get; }

        public override byte[] ToByteArray()
        {
            byte[] result = null;

            using (var steam = new MemoryStream())
            {
                steam.WriteByte((byte)Type);
                ToByteArray(steam);
                var data = steam.ToArray();

                result = new byte[data.Length + 4];
                BitConverter.GetBytes(data.Length).CopyTo(result, 0);

                data.CopyTo(result, 4);
            }

            return result;
        }
    }
}
