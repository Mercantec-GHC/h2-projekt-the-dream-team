using H2Projekt.Application.Dto.Guests;

namespace H2Projekt.API.Context
{
    public class WorkContext
    {
        public GuestDto Guest { get; }

        public WorkContext(int id, string firstName, string lastName, string email)
        {
            Guest = new GuestDto(id, firstName, lastName, email);
        }

        public bool IsAdmin()
        {
            return Guest.Id <= 2;
        }
    }
}
