using FluentValidation;
using H2Projekt.Domain;

namespace H2Projekt.Application.Validators.Bookings
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(b => b.GuestId)
                .GreaterThan(0)
                .WithMessage("Gæst ID'et skal være større end 0.");
            RuleFor(b => b.RoomTypeId)
                .GreaterThan(0)
                .WithMessage("Værelsestype ID'et skal være større end 0.");
            RuleFor(b => b.FromDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .When(b => b.Id == 0)
                .WithMessage("Fra dato skal være i dag eller i fremtiden for nye bookinger.");
            RuleFor(b => b.FromDate)
                .LessThanOrEqualTo(b => b.ToDate)
                .WithMessage("Fra dato skal være tidligere eller lig med Til dato.");
            RuleFor(b => b.ToDate)
                .GreaterThanOrEqualTo(b => b.FromDate)
                .WithMessage("Til dato skal være senere eller lig med Fra dato.");
            RuleFor(b => b.Room)
                .Must((booking, room) => room is null || room.RoomTypeId == booking.RoomTypeId)
                .WithMessage("Tildelt værelse skal være af samme type som bookingens værelsestype.");
        }
    }
}
