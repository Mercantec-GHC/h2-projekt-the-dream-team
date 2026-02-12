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
                .WithMessage("From date must be in the future.");
            RuleFor(b => b.FromDate)
                .LessThan(b => b.ToDate)
                .WithMessage("From date must be earlier than To date.");
            RuleFor(b => b.ToDate)
                .GreaterThan(b => b.FromDate)
                .WithMessage("To date must be later than the From date.");
            //RuleFor(b => b.AssignedRoom).Must((booking, room) =>
            //{
            //    if (room is not null)
            //    {
            //        return room.Type == booking.RoomType;
            //    }

            //    return true;
            //}).WithMessage("AssignedRoom type must match the Booking's RoomType.");
        }
    }
}
