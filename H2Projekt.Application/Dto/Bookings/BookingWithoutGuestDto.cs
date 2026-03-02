using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Domain;

namespace H2Projekt.Application.Dto.Bookings
{
    public class BookingWithoutGuestDto
    {
        public int Id { get; set; }
        public RoomTypeDto RoomType { get; set; } = default!;
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public decimal PriceLocked { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public bool TravelingWithPets { get; set; }
        public RoomWithoutBookingsDto? Room { get; set; }

        public BookingWithoutGuestDto(Booking booking)
        {
            Id = booking.Id;
            RoomType = new RoomTypeDto(booking.RoomType);
            FromDate = booking.FromDate;
            ToDate = booking.ToDate;
            PriceLocked = booking.PriceLocked;
            NumberOfAdults = booking.NumberOfAdults;
            NumberOfChildren = booking.NumberOfChildren;
            TravelingWithPets = booking.TravelingWithPets;

            if (booking.Room is not null)
            {
                Room = new RoomWithoutBookingsDto(booking.Room);
            }
        }
    }
}
