using H2Projekt.Domain.Enums;

namespace H2Projekt.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; } = default!;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }  // fast pris!
        public RoomAvailabilityStatus Status { get; set; }
        public Room() { }

        public Room(string number, int capacity, decimal pricePerNight)
        {
            Number = number;
            Capacity = capacity;
            PricePerNight = pricePerNight;
        }
    }
}
