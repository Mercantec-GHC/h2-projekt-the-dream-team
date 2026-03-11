using H2Projekt.Application.Dto.Guests;

namespace H2Projekt.API
{
    public class WorkContext : GuestDto
    {
        public WorkContext(int id, string firstName, string lastName, string email) : base(id, firstName, lastName, email)
        {
        }

        public bool IsAdmin()
        {
            return Id <= 2;
        }
    }
}
