using FluentValidation;
using H2Projekt.Application.Validators.Rooms;
using H2Projekt.Domain;

namespace H2Projekt.Application.Validators.Bookings
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(b => b.GuestId)
                .GreaterThan(0)
                .WithMessage("GuestId must be a positive integer.");
            RuleFor(b => b.RoomTypeId)
                .GreaterThan(0)
                .WithMessage("RoomTypeId must be a positive integer.");
            RuleFor(b => b.FromDate)
                .GreaterThan(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage("FromDate must be in the future.");
            RuleFor(b => b.FromDate)
                .LessThan(b => b.ToDate)
                .WithMessage("FromDate must be earlier than ToDate.");
            RuleFor(b => b.ToDate)
                .GreaterThan(b => b.FromDate)
                .WithMessage("ToDate must be later than FromDate.");
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
