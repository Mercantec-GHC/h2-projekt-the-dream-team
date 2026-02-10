namespace H2Projekt.Domain
{
    public class RoomType : EntityBase
    {
        public string Name { get; private set; } = default!;
        public string? Description { get; private set; }
        public int MaxOccupancy { get; private set; }

        public RoomType() { }

        public RoomType(string name, string? description, int maxOccupancy)
        {
            Name = name;
            Description = description;
            MaxOccupancy = maxOccupancy;
        }

        public void UpdateDetails(string name, string? description, int maxOccupancy)
        {
            Name = name;
            Description = description;
            MaxOccupancy = maxOccupancy;
        }
    }
}
