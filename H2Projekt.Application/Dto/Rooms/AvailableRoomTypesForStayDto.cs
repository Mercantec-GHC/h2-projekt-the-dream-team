namespace H2Projekt.Application.Dto.Rooms
{
    public class AvailableRoomTypesForStayDto
    {
        public List<RoomTypeDto> SuitableRoomTypes { get; set; } = new List<RoomTypeDto>();
        public List<RoomTypeDto> OtherRoomTypes { get; set; } = new List<RoomTypeDto>();
        public List<RoomTypeDto> UnavailableRoomTypes { get; set; } = new List<RoomTypeDto>();

        public AvailableRoomTypesForStayDto() { }
    }
}
