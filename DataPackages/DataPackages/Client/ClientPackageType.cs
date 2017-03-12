namespace Shared.DataPackages.Client
{
    public enum ClientPackageType : byte
    {
        None,
        Ping,
        Login,
        GetRoom,
        SetTargets,
        ExitFromRoom
    }
}
