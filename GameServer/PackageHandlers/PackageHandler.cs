using Shared.DataPackages.Client;

namespace GameServer.PackageHandlers
{
    public abstract class PackageHandler
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        protected readonly Client Client;
        protected readonly ClientPackage Package;

        protected PackageHandler(Client client, ClientPackage package)
        {
            Client = client;
            Package = package;
        }

        public abstract void HandlePackage();

        public static PackageHandler GetPackageHandler(Client client, ClientPackage package)
        {
            switch (package.Type)
            {
                case ClientPackageType.Ping:
                    return new PingPackageHandler(client, package);
                case ClientPackageType.Login:
                    return new LoginPackageHandler(client, package);
                case ClientPackageType.GetRoom:
                    return new GetRoomPackageHandler(client, package);
                case ClientPackageType.SetTargets:
                    return new SetTargetsPackageHandler(client, package);
                case ClientPackageType.ExitFromRoom:
                    return new ExitFromRoomPackageHandler(client, package);
            }

            return null;
        }
    }
}
