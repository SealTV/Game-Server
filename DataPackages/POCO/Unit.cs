namespace Shared.POCO
{
    public class Unit
    {
        public int Id { get; set; }
        public States State { get; set; }
        public Position Position { get; set; }
        public Position TargetPosition { get; set; }
        public PositionF PositionF { get; set; }
    }
}
