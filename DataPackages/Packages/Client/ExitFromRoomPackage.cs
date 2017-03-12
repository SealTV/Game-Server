using System;
using System.IO;

namespace Shared.Packages.Client
{
    public class ExitFromRoomPackage : ClientPackage
    {
        public override ClientPackageType Type => ClientPackageType.ExitFromRoom;

        public int RoomId { get; set; }

        protected override void ToByteArray(Stream stream)
        {
            stream.Write(BitConverter.GetBytes(RoomId), 0, 4);
        }

        public override void FromByteArray(byte[] array)
        {
            RoomId = BitConverter.ToInt32(array, 0);
        }
    }
}