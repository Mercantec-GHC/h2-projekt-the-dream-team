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

        public int? RoomId { get; private set; }
        public Room? Room { get; private set; }

        public Booking() { }

        public Booking(int guestId, int roomTypeId, DateOnly fromDate, DateOnly toDate, decimal priceLocked)
        {
            GuestId = guestId;
            RoomTypeId = roomTypeId;
            FromDate = fromDate;
            ToDate = toDate;
            PriceLocked = priceLocked;
        }

        public void AssignRoom(Room room)
        {
            RoomId = room.Id;
            Room = room;
        }
    }
}