using Shared.DataPackages.Client;
using Shared.DataPackages.Server;

namespace GameServer.PackageHandlers
{
    public sealed class LoginPackageHandler : PackageHandler
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public LoginPackageHandler(Client client, ClientPackage package) : base(client, package)
        {}

        public override void HandlePackage()
        {
            Client.ClientId = Client.ClientsCount;
            Client.SendPackage(new AcceptLoginPackage
            {
                ClientId = Client.ClientId
            });
        }
    }
}