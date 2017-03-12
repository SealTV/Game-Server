namespace Shared.Packages.Client
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
