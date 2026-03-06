using H2Projekt.Application.Dto.Rooms;

namespace H2Projekt.Application.Dto.Bookings
{
    public class BookingOverviewDto
    {
        public List<DateOnly> Dates { get; set; } = new List<DateOnly>();
        public List<RoomTypeDto> RoomTypes { get; set; } = new List<RoomTypeDto>();
        public List<BookingOverviewBookingDto> Bookings { get; set; } = new List<BookingOverviewBookingDto>();
    }
}
