using H2Projekt.Domain.Enums;

namespace H2Projekt.Domain
{
    public class Room : EntityBase
    {
        public string Number { get; private set; } = default!;

        public int RoomTypeId { get; private set; }
        public RoomType RoomType { get; private set; } = default!;

        public RoomAvailabilityStatus Status { get; private set; }
        public ICollection<Booking> Bookings { get; private set; } = new List<Booking>();

        public Room() { }

        public Room(string number, int roomTypeId, RoomAvailabilityStatus status)
        {
            Number = number;
            RoomTypeId = roomTypeId;
            Status = status;
        }

        public void UpdateDetails(string number, int roomTypeId, RoomAvailabilityStatus status)
        {
            Number = number;
            RoomTypeId = roomTypeId;
            Status = status;
        }
    }
}
