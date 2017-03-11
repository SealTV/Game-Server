namespace Shared.POCO
{
    public class Room
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Unit[] Units { get; set; }

        public Room()
        {
            Units = new Unit[0];
        }
    }
}
