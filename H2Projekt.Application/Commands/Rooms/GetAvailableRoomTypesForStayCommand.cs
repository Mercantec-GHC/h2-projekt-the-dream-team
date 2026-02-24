namespace H2Projekt.Application.Commands.Rooms
{
    public class GetAvailableRoomTypesForStayCommand
    {
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public bool TravelingWithPets { get; set; }
    }
}
