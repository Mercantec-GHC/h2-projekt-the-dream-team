using H2Projekt.Domain;

namespace H2Projekt.Application.Commands
{
    public class CreateBookingCommand
    {
        public Guid GuestId { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
