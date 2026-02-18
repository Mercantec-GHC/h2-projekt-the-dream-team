namespace H2Projekt.Application.Commands.Rooms
{
    public class CreateRoomDiscountCommand
    {
        public int RoomTypeId { get; set; }
        public string Description { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public int PricePerNight { get; set; }
    }
}
