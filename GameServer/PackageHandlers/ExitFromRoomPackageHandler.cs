using System;
using Shared.DataPackages.Client;

namespace GameServer.PackageHandlers
{
    public sealed class ExitFromRoomPackageHandler : PackageHandler
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public ExitFromRoomPackageHandler(Client client, ClientPackage package) : base(client, package)
        {
        }

        public override void HandlePackage()
        {
            Logger.Info($"Disconnect client ${Client.ClientId}");
            Client.Disconnect();
        }
    }
}