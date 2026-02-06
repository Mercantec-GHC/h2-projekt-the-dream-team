using H2Projekt.Domain;
using H2Projekt.Domain.Enums;

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
