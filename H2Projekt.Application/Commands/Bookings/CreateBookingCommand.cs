namespace H2Projekt.Application.Commands.Bookings
{
    public class CreateBookingCommand
    {
        public int GuestId { get; set; }
        public int RoomTypeId { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
    }
}
