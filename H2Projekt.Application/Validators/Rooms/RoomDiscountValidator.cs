using FluentValidation;
using H2Projekt.Domain;

namespace H2Projekt.Application.Validators.Rooms
{
    public class RoomDiscountValidator : AbstractValidator<RoomDiscount>
    {
        public RoomDiscountValidator()
        {
            RuleFor(roomDiscount => roomDiscount.RoomTypeId)
                .GreaterThan(0)
                .WithMessage("Værelsestype ID'et skal være større end 0.");
            RuleFor(roomDiscount => roomDiscount.Description)
                .NotEmpty()
                .WithMessage("Beskrivelse er påkrævet.")
                .MaximumLength(200)
                .WithMessage("Beskrivelse må inte overstige 200 tecken.");
            RuleFor(roomDiscount => roomDiscount.FromDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .When(roomDiscount => roomDiscount.Id == 0)
                .WithMessage("Fra dato skal være i dag eller i fremtiden for nye tilbud.");
            RuleFor(roomDiscount => roomDiscount.FromDate)
                .LessThanOrEqualTo(roomDiscount => roomDiscount.ToDate)
                .WithMessage("Fra dato skal være tidligere eller lig med Til dato.");
            RuleFor(roomDiscount => roomDiscount.ToDate)
                .GreaterThanOrEqualTo(roomDiscount => roomDiscount.FromDate)
                .WithMessage("Til dato skal være senere eller lig med Fra dato.");
        }
    }
}
