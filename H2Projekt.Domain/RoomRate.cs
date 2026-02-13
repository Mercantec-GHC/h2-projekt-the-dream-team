namespace H2Projekt.Domain
{
    public class RoomRate : EntityBase
    {
        public int RoomTypeId { get; private set; }
        public RoomType RoomType { get; private set; } = default!;

        public DateOnly FromDate { get; private set; }
        public DateOnly ToDate { get; private set; }

        public decimal PricePerNight { get; private set; }

        public RoomRate() { }
    }
}
