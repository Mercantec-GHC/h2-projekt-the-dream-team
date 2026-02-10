namespace H2Projekt.Application.Commands
{
    public class UpdateRoomTypeCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxOccupancy { get; set; }
    }
}
