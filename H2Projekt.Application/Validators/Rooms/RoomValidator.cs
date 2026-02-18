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
                .WithMessage("Værelsesnummer er påkrævet.")
                .Matches("[0-9]+.[0-9]+")
                .WithMessage("Værelsesnummer skal være i formatet 'X.Y' hvor X og Y er tal.");
            RuleFor(room => room.RoomTypeId)
                .GreaterThan(0)
                .WithMessage("Værelsestype ID'et skal være større end 0.");
            RuleFor(room => room.Status)
                .IsInEnum()
                .WithMessage("Status skal være en gyldig enum værdi.");
        }
    }
}
