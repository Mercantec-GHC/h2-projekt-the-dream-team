namespace H2Projekt.Application.Commands.Rooms
{
    public class CreateRoomTypeCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxOccupancy { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
