using H2Projekt.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace H2Projekt.Domain
{
    public class Room : EntityBase
    {
        public string Number { get; private set; } = default!;
        [Range(0, int.MaxValue)]
        public int Capacity { get; private set; }
        [Range(0, 10000)]
        public decimal PricePerNight { get; private set; }  // fast pris!
        public RoomAvailabilityStatus Status { get; private set; }

        public Room() { }

        public Room(string number, int capacity, decimal pricePerNight)
        {
            Number = number;
            Capacity = capacity;
            PricePerNight = pricePerNight;
        }

        public void UpdateDetails(string number, int capacity, decimal pricePerNight)
        {
            Number = number;
            Capacity = capacity;
            PricePerNight = pricePerNight;
        }
    }
}
