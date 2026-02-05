using H2Projekt.Domain.Enums;

namespace H2Projekt.Application.Commands
{
    public class CreateRoomCommand
    {
        public string Number { get; set; }
        public RoomType Type { get; set; }
        public decimal PricePerNight { get; set; }
    }
}