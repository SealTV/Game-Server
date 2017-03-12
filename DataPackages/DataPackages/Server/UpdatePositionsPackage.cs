using System;
using System.IO;
using Shared.POCO;

namespace Shared.DataPackages.Server
{
   public sealed class UpdatePositionsPackage : ServePackage
    {
        public override ServerPackageType Type => ServerPackageType.UpdatePositions;

        public Unit[] Units { get; set; }

        public UpdatePositionsPackage()
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
