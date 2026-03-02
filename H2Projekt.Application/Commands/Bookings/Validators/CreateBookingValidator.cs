using FluentValidation;

namespace H2Projekt.Application.Commands.Bookings.Validators
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingValidator()
        {
            RuleFor(request => request.GuestId)
                .GreaterThan(0)
                .WithMessage("Gæst ID'et skal være større end 0.");
            RuleFor(request => request.RoomTypeId)
                .GreaterThan(0)
                .WithMessage("Værelsestype ID'et skal være større end 0.");
            RuleFor(request => request.FromDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("Fra dato skal være i dag eller i fremtiden for nye bookinger.");
            RuleFor(request => request.FromDate)
                .LessThanOrEqualTo(request => request.ToDate)
                .WithMessage("Fra dato skal være tidligere eller lig med Til dato.");
            RuleFor(request => request.ToDate)
                .GreaterThanOrEqualTo(request => request.FromDate)
                .WithMessage("Til dato skal være senere eller lig med Fra dato.");
            RuleFor(request => request.NumberOfAdults)
                .GreaterThanOrEqualTo(1)
                .When(request => request.NumberOfChildren == 0)
                .WithMessage("Antal voksne skal være mindst 1, hvis der ikke er børn.");
            RuleFor(request => request.NumberOfChildren)
                .GreaterThanOrEqualTo(1)
                .When(request => request.NumberOfAdults == 0)
                .WithMessage("Antal børn skal være mindst 1, hvis der ikke er voksne.");
        }
    }
}
