using FluentValidation;
using H2Projekt.Domain;

namespace H2Projekt.Application.Validators.Rooms
{
    public class RoomValidator : AbstractValidator<Room>
    {
        public RoomValidator()
        {
            RuleFor(room => room.Number)
                .NotEmpty()
                .WithMessage("Room number is required.")
                .Matches("[0-9]+.[0-9]+")
                .WithMessage("Room number must be in the format 'X.Y' where X and Y are numbers.");
            RuleFor(room => room.RoomTypeId)
                .GreaterThan(0)
                .WithMessage("Room type ID must be greater than 0.");
            RuleFor(room => room.Status)
                .IsInEnum()
                .WithMessage("Invalid room status.");
        }
    }
}
