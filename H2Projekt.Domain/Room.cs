using H2Projekt.Domain.Enums;
namespace H2Projekt.Domain
{
    public class Room : EntityBase
    {
        public string Number { get; private set; } = default!;
        public RoomType Type { get; private set; }
        public decimal PricePerNight { get; private set; }
        public RoomAvailabilityStatus Status { get; private set; }

        public Room() { }

        public Room(string number, RoomType type, decimal pricePerNight)
        {
            Number = number;
            Type = type;
            PricePerNight = pricePerNight;
        }

        public void UpdateDetails(string number, RoomType type, decimal pricePerNight)
        {
            Number = number;
            Type = type;
            PricePerNight = pricePerNight;
        }
    }
}
