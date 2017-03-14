using Shared.DataPackages.Client;

namespace GameServer.PackageHandlers
{
    public sealed class SetTargetsPackageHandler : PackageHandler
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public SetTargetsPackageHandler(Client client, ClientPackage package) : base(client, package)
        {
        }

        public override void HandlePackage()
        {
            Logger.Debug("Handle SetTargetsPackage package");
            SetTargetsPackage targetsPackage = (SetTargetsPackage) Package;
            Client.Game?.SetTargets(targetsPackage.Units);
        }
    }
}