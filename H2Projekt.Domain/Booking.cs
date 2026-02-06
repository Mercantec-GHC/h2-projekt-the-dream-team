using H2Projekt.Domain.Enums;

namespace H2Projekt.Domain
{
    public class Booking : EntityBase
    {
        public Guest Guest { get; private set; } = default!;
        public RoomType RoomType { get; private set; }
        public DateTimeOffset FromDate { get; private set; }
        public DateTimeOffset ToDate { get; private set; }
        public Room? AssignedRoom { get; private set; }

        public Booking() { }

        public Booking(Guest guest, RoomType roomType, DateTimeOffset checkInDate, DateTimeOffset checkOutDate)
        {
            Guest = guest;
            RoomType = roomType;
            FromDate = checkInDate;
            ToDate = checkOutDate;
        }
    }
}