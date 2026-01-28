namespace H2Projekt.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Number { get; set; } = default!;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }  // fast pris!
    }
}
