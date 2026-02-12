using H2Projekt.Domain;

namespace H2Projekt.Application.Commands.Bookings
{
    public class CreateBookingCommand
    {
        public int GuestId { get; set; }
        public RoomType RoomType { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
    }
}
