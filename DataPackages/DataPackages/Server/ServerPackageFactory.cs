using System;
using System.IO;

namespace Shared.DataPackages.Server
{
    public sealed class ServerPackageFactory
    {
        public ServePackage GetNextPackage(Stream stream)
        {
            ServePackage result = null;

            byte[] data = new byte[4];
            stream.Read(data, 0, 4);
            int len = BitConverter.ToInt32(data, 0);
            if (len <= 0)
                return null;

            ServerPackageType type = (ServerPackageType) stream.ReadByte();
          
            data = new byte[len - 1];
            stream.Read(data, 0, len - 1);
            switch (type)
            {
                case ServerPackageType.Pong:
                    result = new PongPackage();
                    break;
                case ServerPackageType.AcceptLogin:
                    result = new AcceptLoginPackage();
                    break;
                case ServerPackageType.SetRoom:
                    result = new SetRoomPackage();
                    break;
                case ServerPackageType.UpdatePositions:
                    result = new UpdatePositionsPackage();
                    break;
            }

            result.FromByteArray(data);
            return result;
        }

        public ServePackage GetNextPackage(byte[] data)
        {
            ServePackage result = null;
            using (var stream = new MemoryStream(data))
            {
                result = GetNextPackage(stream);
            }
            return result;
        }
    }
}
