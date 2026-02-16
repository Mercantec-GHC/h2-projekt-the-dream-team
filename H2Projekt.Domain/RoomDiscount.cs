namespace H2Projekt.Domain
{
    public class RoomDiscount : EntityBase
    {
        public int RoomTypeId { get; private set; }
        public RoomType RoomType { get; private set; } = default!;

        public string Description { get; private set; }

        public DateOnly FromDate { get; private set; }
        public DateOnly ToDate { get; private set; }

        public int Percentage { get; private set; }

        public RoomDiscount() { }
    }
}
