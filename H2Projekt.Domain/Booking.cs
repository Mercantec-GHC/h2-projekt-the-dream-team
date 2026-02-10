namespace H2Projekt.Domain
{
    public class Booking : EntityBase
    {
        public Guest Guest { get; private set; } = default!;
        public RoomType RoomType { get; private set; }
        public DateOnly FromDate { get; private set; }
        public DateOnly ToDate { get; private set; }
        public decimal PriceLocked { get; private set; }
        public Room? AssignedRoom { get; private set; }

        public Booking() { }

        public Booking(Guest guest, RoomType roomType, DateOnly fromDate, DateOnly toDate, decimal priceLocked)
        {
            Guest = guest;
            RoomType = roomType;
            FromDate = fromDate;
            ToDate = toDate;
            PriceLocked = priceLocked;
        }
    }
}