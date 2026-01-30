namespace H2Projekt.Domain
{
    public class Booking
    {
        public int Id { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; } = default!;

        public int RoomId { get; set; }
        public Room Room { get; set; } = default!;

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
