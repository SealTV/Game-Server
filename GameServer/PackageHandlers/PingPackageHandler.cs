using Shared.DataPackages.Client;
using Shared.DataPackages.Server;

namespace GameServer.PackageHandlers
{
    public class PingPackageHandler : PackageHandler
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public PingPackageHandler(Client client, ClientPackage package) : base(client, package)
        {}

        public override void HandlePackage()
        {
            PingPackage pingPackage = (PingPackage) Package;

            Logger.Debug($"Ping package value: {pingPackage.Value}");

            Client.SendPackage(new PongPackage
            {
                Value = pingPackage.Value
            });
        }
    }
}
