using H2Projekt.Application.Dto.Guests;
using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Domain;

namespace H2Projekt.Application.Dto.Bookings
{
    public class BookingDto
    {
        public GuestWithoutBookingsDto Guest { get; private set; } = default!;
        public RoomType RoomType { get; set; } = default!;
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public decimal PriceLocked { get; set; }
        public RoomWithoutRoomTypeDto? Room { get; set; }

        public BookingDto(Booking booking)
        {
            Guest = new GuestWithoutBookingsDto(booking.Guest);
            RoomType = booking.RoomType;
            FromDate = booking.FromDate;
            ToDate = booking.ToDate;
            PriceLocked = booking.PriceLocked;
            Room = new RoomWithoutRoomTypeDto(booking.Room);
        }
    }
}
