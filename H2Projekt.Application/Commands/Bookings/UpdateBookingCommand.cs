namespace H2Projekt.Application.Commands.Bookings
{
    public class UpdateBookingCommand
    {
        public int GuestId { get; set; }
        public int BookingId { get; set; }
        public int AssignedRoomId { get; set; }
    }
}
