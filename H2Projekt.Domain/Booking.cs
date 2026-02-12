namespace H2Projekt.Domain
{
    public class Booking : EntityBase
    {
        public int GuestId { get; private set; }
        public Guest Guest { get; private set; } = default!;

        public int RoomTypeId { get; private set; }
        public RoomType RoomType { get; private set; }

        public DateOnly FromDate { get; private set; }
        public DateOnly ToDate { get; private set; }
        public decimal PriceLocked { get; private set; }

        public int? AssignedRoomId { get; private set; }
        public Room? AssignedRoom { get; private set; }

        public Booking() { }

        public Booking(int guestid, int roomTypeid, DateOnly fromDate, DateOnly toDate, decimal priceLocked)
        {
            GuestId = guestid;
            RoomTypeId = roomTypeid;
            FromDate = fromDate;
            ToDate = toDate;
            PriceLocked = priceLocked;
        }
    }
}