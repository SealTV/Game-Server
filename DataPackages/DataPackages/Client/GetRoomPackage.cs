using System.IO;

namespace Shared.DataPackages.Client
{
    public class GetRoomPackage : ClientPackage
    {
        public override ClientPackageType Type => ClientPackageType.GetRoom;

        protected override void ToByteArray(Stream stream)
        {}

        public override void FromByteArray(byte[] data)
        {}
    }
}