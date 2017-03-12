using System;
using System.IO;

namespace Shared.DataPackages.Client
{
    public class ExitFromRoomPackage : ClientPackage
    {
        public override ClientPackageType Type => ClientPackageType.ExitFromRoom;

        public int RoomId { get; set; }

        protected override void ToByteArray(Stream stream)
        {
            stream.Write(BitConverter.GetBytes(RoomId), 0, 4);
        }

        public override void FromByteArray(byte[] data)
        {
            RoomId = BitConverter.ToInt32(data, 0);
        }
    }
}