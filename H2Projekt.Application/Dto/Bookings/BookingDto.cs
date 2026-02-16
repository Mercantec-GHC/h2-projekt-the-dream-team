using H2Projekt.Application.Dto.Guests;
using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Domain;

namespace H2Projekt.Application.Dto.Bookings
{
    public class BookingDto
    {
        public int Id { get; set; }
        public GuestWithoutBookingsDto Guest { get; private set; } = default!;
        public RoomTypeDto RoomType { get; set; } = default!;
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public decimal PriceLocked { get; set; }
        public RoomWithoutBookingsDto? Room { get; set; }

        public BookingDto(Booking booking)
        {
            Id = booking.Id;
            Guest = new GuestWithoutBookingsDto(booking.Guest);
            RoomType = new RoomTypeDto(booking.RoomType);
            FromDate = booking.FromDate;
            ToDate = booking.ToDate;
            PriceLocked = booking.PriceLocked;

            if (booking.Room is not null)
            {
                Room = new RoomWithoutBookingsDto(booking.Room);
            }
        }
    }
}
