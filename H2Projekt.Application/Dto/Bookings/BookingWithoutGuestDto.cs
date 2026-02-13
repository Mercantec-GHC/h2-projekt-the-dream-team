using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Domain;

namespace H2Projekt.Application.Dto.Bookings
{
    public class BookingWithoutGuestDto
    {
        public RoomTypeDto RoomType { get; set; } = default!;
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public decimal PriceLocked { get; set; }
        public RoomWithoutBookingsDto? Room { get; set; }

        public BookingWithoutGuestDto(Booking booking)
        {
            RoomType = new RoomTypeDto(booking.RoomType);
            FromDate = booking.FromDate;
            ToDate = booking.ToDate;
            PriceLocked = booking.PriceLocked;
            Room = new RoomWithoutBookingsDto(booking.Room);
        }
    }
}
