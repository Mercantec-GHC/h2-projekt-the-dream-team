using H2Projekt.Domain;
using H2Projekt.Domain.Enums;

namespace H2Projekt.Application.Dto.Rooms
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = default!;
        public RoomType RoomType { get; set; } = default!;
        public RoomAvailabilityStatus Status { get; set; }
        public ICollection<Booking> Bookings { get; set; } = default!;

        public RoomDto(Room room)
        {
            Id = room.Id;
            Number = room.Number;
            RoomType = room.RoomType;
            Status = room.Status;
            Bookings = room.Bookings;
        }
    }
}
