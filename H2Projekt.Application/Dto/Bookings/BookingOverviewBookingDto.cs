using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Domain;

namespace H2Projekt.Application.Dto.Bookings
{
    public class BookingOverviewBookingDto : BookingDto
    {
        public RoomDto? PossibleRoom { get; set; }

        public BookingOverviewBookingDto(Booking booking) : base(booking)
        {
        }
    }
}
