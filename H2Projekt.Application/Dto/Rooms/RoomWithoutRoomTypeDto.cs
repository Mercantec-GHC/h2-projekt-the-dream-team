using H2Projekt.Domain;
using H2Projekt.Domain.Enums;

namespace H2Projekt.Application.Dto.Rooms
{
    public class RoomWithoutRoomTypeDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = default!;
        public RoomAvailabilityStatus Status { get; set; }
        public ICollection<Booking> Bookings { get; set; } = default!;

        public RoomWithoutRoomTypeDto(Room? room)
        {
            if (room is null)
            {
                return;
            }

            Id = room.Id;
            Number = room.Number;
            Status = room.Status;
            Bookings = room.Bookings;
        }
    }
}
