using FluentValidation;

namespace H2Projekt.Application.Commands.Rooms.Validators
{
    public class CreateRoomDiscountValidator : AbstractValidator<CreateRoomDiscountCommand>
    {
        public CreateRoomDiscountValidator()
        {
            RuleFor(request => request.RoomTypeId)
                .GreaterThan(0)
                .WithMessage("Værelsestype ID'et skal være større end 0.");
            RuleFor(request => request.Description)
                .NotEmpty()
                .WithMessage("Beskrivelse er påkrævet.")
                .MaximumLength(200)
                .WithMessage("Beskrivelse må ikke overstige 200 tegn.");
            RuleFor(request => request.FromDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Fra dato skal være i dag eller i fremtiden for nye værelsestilbud.");
            RuleFor(request => request.FromDate)
                .LessThanOrEqualTo(request => request.ToDate)
                .WithMessage("Fra dato skal være tidligere eller lig med Til dato.");
            RuleFor(request => request.ToDate)
                .GreaterThanOrEqualTo(request => request.FromDate)
                .WithMessage("Til dato skal være senere eller lig med Fra dato.");
            RuleFor(request => request.PricePerNight)
                .GreaterThan(0)
                .WithMessage("Pris per nat skal være større end 0.");
        }
    }
}
