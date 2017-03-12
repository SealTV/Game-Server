using System.IO;
using Shared.POCO;

namespace Shared.DataPackages.Server
{
   public sealed class SetRoomPackage : ServePackage
    {
        public override ServerPackageType Type => ServerPackageType.SetRoom;

        public Room Room { get; set; }

        protected override void ToByteArray(Stream stream)
        {
            var data = Room.ToByteArray();
            stream.Write(data, 0, data.Length);
        }

        public override void FromByteArray(byte[] data)
        {
            Room = new Room();
            Room.FromBytes(data);
        }
    }
}
