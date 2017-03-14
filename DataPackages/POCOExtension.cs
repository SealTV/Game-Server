using System;
using System.IO;
using Shared.POCO;

namespace Shared
{
    public static class POCOExtension
    {
        public static byte[] ToByteArray(this Position p)
        {
            byte[] data = new byte[8];
            BitConverter.GetBytes(p.X).CopyTo(data, 0);
            BitConverter.GetBytes(p.Y).CopyTo(data, 4);
            return data;
        }

        public static Position FromBytes(this Position p, byte[] data)
        {
            p.X = BitConverter.ToInt32(data, 0);
            p.Y = BitConverter.ToInt32(data, 4);
            return p;
        }

        public static byte[] ToByteArray(this PositionF p)
        {
            byte[] data = new byte[8];
            BitConverter.GetBytes(p.X).CopyTo(data, 0);
            BitConverter.GetBytes(p.Y).CopyTo(data, 4);
            return data;
        }

        public static PositionF FromBytes(this PositionF p, byte[] data)
        {
            p.X = BitConverter.ToSingle(data, 0);
            p.Y = BitConverter.ToSingle(data, 4);
            return p;
        }

        public static byte[] ToByteArray(this Unit unit)
        {
            byte[] data = null;

            using (var stream = new MemoryStream())
            {
                stream.Write(BitConverter.GetBytes(unit.Id), 0, 4);
                stream.WriteByte((byte) unit.State);
                stream.Write(unit.Position.ToByteArray(), 0, 8);
                stream.Write(unit.TargetPosition.ToByteArray(), 0, 8);
                stream.Write(unit.PositionF.ToByteArray(), 0, 8);

                data = stream.ToArray();
            }

            return data;
        }

        public static void FromBytes(this Unit unit, byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                using (var readed = new BinaryReader(stream))
                {
                    unit.Id = readed.ReadInt32();

                    unit.State = (States) readed.ReadByte();
                    unit.Position = new Position();
                    unit.Position = unit.Position.FromBytes(readed.ReadBytes(8));
                    unit.TargetPosition = new Position();
                    unit.TargetPosition = unit.TargetPosition.FromBytes(readed.ReadBytes(8));
                    unit.PositionF = new PositionF();
                    unit.PositionF = unit.PositionF.FromBytes(readed.ReadBytes(8));
                }
            }
        }

        public static byte[] ToByteArray(this Unit[] units)
        {
            byte[] data = null;

            using (var stream = new MemoryStream())
            {
                stream.Write(BitConverter.GetBytes(units.Length), 0, 4);

                for (int i = 0; i < units.Length; i++)
                {
                    var array = units[i].ToByteArray();
                    stream.Write(BitConverter.GetBytes(array.Length), 0, 4);
                    stream.Write(array, 0, array.Length);
                }

                data = stream.ToArray();
            }
            
            return data;
        }

        public static Unit[] FromBytes(this Unit[] units, byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                using (var readed = new BinaryReader(stream))
                {
                    int unitsCount = readed.ReadInt32();
                    units = new Unit[unitsCount];
                    for (int i = 0; i < unitsCount; i++)
                    {
                        var len = readed.ReadInt32();
                        byte[] bytes = readed.ReadBytes(len);
                        units[i] = new Unit();
                        units[i].FromBytes(bytes);
                    }
                }
            }

            return units;
        }

        public static byte[] ToByteArray(this Room room)
        {
            byte[] data = null;

            using (var stream = new MemoryStream())
            {
                stream.Write(BitConverter.GetBytes(room.Id), 0, 4);
                stream.Write(BitConverter.GetBytes(room.Width), 0, 4);
                stream.Write(BitConverter.GetBytes(room.Height), 0, 4);
                var bytes = room.Units.ToByteArray();
                stream.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
                stream.Write(bytes, 0, bytes.Length);

                data = stream.ToArray();
            }
            
            return data;
        }

        public static void FromBytes(this Room room, byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                using (var readed = new BinaryReader(stream))
                {
                    room.Id = readed.ReadInt32();
                    room.Width = readed.ReadInt32();
                    room.Height = readed.ReadInt32();
                    int len = readed.ReadInt32();
                    var bytes = readed.ReadBytes(len);
                    room.Units = room.Units.FromBytes(bytes);
                }
            }
        }
    }
}
