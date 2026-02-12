using H2Projekt.Domain;

namespace H2Projekt.Application.Dto.Bookings
{
    public class BookingWithoutGuestDto
    {
        public RoomType RoomType { get; set; } = default!;
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public decimal PriceLocked { get; set; }
        public Room? AssignedRoom { get; set; }

        public BookingWithoutGuestDto(Booking booking)
        {
            RoomType = booking.RoomType;
            FromDate = booking.FromDate;
            ToDate = booking.ToDate;
            PriceLocked = booking.PriceLocked;
            AssignedRoom = booking.AssignedRoom;
        }
    }
}
