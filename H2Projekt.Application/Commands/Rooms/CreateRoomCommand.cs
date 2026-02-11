using H2Projekt.Domain.Enums;

namespace H2Projekt.Application.Commands.Rooms
{
    public class CreateRoomCommand
    {
        public string Number { get; set; }
        public int RoomTypeId { get; set; }
        public RoomAvailabilityStatus Status { get; set; }
    }
}