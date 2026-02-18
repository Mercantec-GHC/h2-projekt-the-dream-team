using FluentValidation;
using H2Projekt.Domain;

namespace H2Projekt.Application.Validators.Bookings
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(booking => booking.GuestId)
                .GreaterThan(0)
                .WithMessage("Gæst ID'et skal være større end 0.");
            RuleFor(booking => booking.RoomTypeId)
                .GreaterThan(0)
                .WithMessage("Værelsestype ID'et skal være større end 0.");
            RuleFor(booking => booking.FromDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .When(booking => booking.Id == 0)
                .WithMessage("Fra dato skal være i dag eller i fremtiden for nye bookinger.");
            RuleFor(booking => booking.FromDate)
                .LessThanOrEqualTo(booking => booking.ToDate)
                .WithMessage("Fra dato skal være tidligere eller lig med Til dato.");
            RuleFor(booking => booking.ToDate)
                .GreaterThanOrEqualTo(booking => booking.FromDate)
                .WithMessage("Til dato skal være senere eller lig med Fra dato.");
            RuleFor(booking => booking.Room)
                .Must((booking, room) => room is null || room.RoomTypeId == booking.RoomTypeId)
                .WithMessage("Tildelt værelse skal være af samme type som bookingens værelsestype.");
        }
    }
}
