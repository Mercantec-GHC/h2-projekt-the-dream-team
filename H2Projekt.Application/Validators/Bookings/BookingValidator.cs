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
                .WithMessage("Guest ID must be greater than 0.");
            RuleFor(b => b.RoomTypeId)
                .GreaterThan(0)
                .WithMessage("Room type ID must be greater than 0.");
            RuleFor(b => b.FromDate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .When(b => b.Id == 0)
                .WithMessage("From date must be in the future.");
            RuleFor(b => b.FromDate)
                .LessThan(b => b.ToDate)
                .WithMessage("From date must be earlier than To date.");
            RuleFor(b => b.ToDate)
                .GreaterThan(b => b.FromDate)
                .WithMessage("To date must be later than the From date.");
            RuleFor(b => b.Room)
                .Must((booking, room) => room is null || room.RoomTypeId == booking.RoomTypeId)
                .WithMessage("Assigned room must be of the same type as the booking's room type.");
        }
    }
}
