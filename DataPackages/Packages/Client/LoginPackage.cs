using System.IO;

namespace Shared.Packages.Client
{
    public sealed class LoginPackage : ClientPackage
    {
        public override ClientPackageType Type => ClientPackageType.Login;

        protected override void ToByteArray(Stream stream)
        {}

        public override void FromByteArray(byte[] array)
        {}
    }
}
