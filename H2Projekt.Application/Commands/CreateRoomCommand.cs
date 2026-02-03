namespace H2Projekt.Application.Commands
{
    public class CreateRoomCommand
    {
        public string Number { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
    }
}