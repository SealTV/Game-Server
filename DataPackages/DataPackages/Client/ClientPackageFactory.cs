using System;
using System.IO;

namespace Shared.DataPackages.Client
{
    public sealed class ClientPackageFactory
    {
        public ClientPackage GetNextPackage(Stream stream)
        {
            ClientPackage result = null;

            byte[] data = new byte[4];
            stream.Read(data, 0, 4);
            int len = BitConverter.ToInt32(data, 0);
            if (len <= 0)
                return null;

            ClientPackageType type = (ClientPackageType) stream.ReadByte();
          
            data = new byte[len - 1];
            stream.Read(data, 0, len - 1);
            switch (type)
            {
                case ClientPackageType.Ping:
                    result = new PingPackage();
                    break;
                case ClientPackageType.Login:
                    result = new LoginPackage();
                    break;
                case ClientPackageType.GetRoom:
                    result = new GetRoomPackage();
                    break;
                case ClientPackageType.SetTargets:
                    result = new SetTargetsPackage();
                    break;
                case ClientPackageType.ExitFromRoom:
                    result = new ExitFromRoomPackage();
                    break;
            }

            result.FromByteArray(data);
            return result;
        }

        public ClientPackage GetNextPackage(byte[] data)
        {
            ClientPackage result = null;
            using (var stream = new MemoryStream(data))
            {
                result = GetNextPackage(stream);
            }
            return result;
        }
    }
}
