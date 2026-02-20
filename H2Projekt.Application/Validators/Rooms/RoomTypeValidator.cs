using FluentValidation;
using H2Projekt.Domain;

namespace H2Projekt.Application.Validators.Rooms
{
    public class RoomTypeValidator : AbstractValidator<RoomType>
    {
        public RoomTypeValidator()
        {
            RuleFor(roomType => roomType.Name)
                .NotEmpty()
                .WithMessage("Navn er påkrævet.");
            RuleFor(roomType => roomType.Description)
                .NotEmpty()
                .WithMessage("Beskrivelse er påkrævet.");
            RuleFor(roomType => roomType.MaxOccupancy)
                .GreaterThan(0)
                .WithMessage("Maksimal belægning skal være større end 0.");
            RuleFor(roomType => roomType.PricePerNight)
                .GreaterThan(0)
                .WithMessage("Pris per nat skal være større end 0.");
        }
    }
}
