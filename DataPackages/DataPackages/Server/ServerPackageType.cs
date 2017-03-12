namespace Shared.DataPackages.Server
{
    public enum ServerPackageType : byte
    {
        None,
        Pong,
        AcceptLogin,
        SetRoom,
        UpdatePositions,
    }
}
