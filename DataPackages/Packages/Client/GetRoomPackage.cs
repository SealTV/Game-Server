using System.IO;

namespace Shared.Packages.Client
{
    public class GetRoomPackage : ClientPackage
    {
        public override ClientPackageType Type => ClientPackageType.GetRoom;

        protected override void ToByteArray(Stream stream)
        {}

        public override void FromByteArray(byte[] array)
        {}
    }
}