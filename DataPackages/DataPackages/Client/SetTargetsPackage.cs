using System;
using System.IO;
using Shared.POCO;

namespace Shared.DataPackages.Client
{
    public class SetTargetsPackage : ClientPackage
    {
        public override ClientPackageType Type => ClientPackageType.SetTargets;

        public Unit[] Units { get; set; }

        public SetTargetsPackage()
        {
            Units = new Unit[0];
        }

        protected override void ToByteArray(Stream stream)
        {
            var bytes = Units.ToByteArray();
            stream.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
            stream.Write(bytes, 0, bytes.Length);
        }

        public override void FromByteArray(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                using (var readed = new BinaryReader(stream))
                {
                    int len = readed.ReadInt32();
                    var bytes = readed.ReadBytes(len);
                    Units = Units.FromBytes(bytes);
                }
            }
        }
    }
}