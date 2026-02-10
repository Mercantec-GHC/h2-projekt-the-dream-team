using H2Projekt.Domain;
using H2Projekt.Domain.Enums;

namespace H2Projekt.Application.Commands
{
    public class UpdateRoomCommand
    {
        public string Number { get; set; }
        public int RoomTypeId { get; set; }
        public RoomAvailabilityStatus Status { get; set; }
    }
}
