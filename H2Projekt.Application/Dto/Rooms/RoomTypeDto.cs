using H2Projekt.Domain;

namespace H2Projekt.Application.Dto.Rooms
{
    public class RoomTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public int MaxOccupancy { get; set; }

        public RoomTypeDto(RoomType roomType)
        {
            Id = roomType.Id;
            Name = roomType.Name;
            Description = roomType.Description;
            MaxOccupancy = roomType.MaxOccupancy;
        }
    }
}
