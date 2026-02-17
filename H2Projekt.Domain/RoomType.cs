namespace H2Projekt.Domain
{
    public class RoomType : EntityBase
    {
        public string Name { get; private set; } = default!;
        public string? Description { get; private set; }
        public int MaxOccupancy { get; private set; }
        public decimal PricePerNight { get; private set; }

        public RoomType() { }

        public RoomType(string name, string? description, int maxOccupancy, decimal pricePerNight)
        {
            Name = name;
            Description = description;
            MaxOccupancy = maxOccupancy;
            PricePerNight = pricePerNight;
        }

        public void UpdateDetails(string name, string? description, int maxOccupancy, decimal pricePerNight)
        {
            Name = name;
            Description = description;
            MaxOccupancy = maxOccupancy;
            PricePerNight = pricePerNight;
        }
    }
}
