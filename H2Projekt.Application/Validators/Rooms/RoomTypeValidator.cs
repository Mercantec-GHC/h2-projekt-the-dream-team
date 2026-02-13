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
                .WithMessage("Room type name is required.");
            RuleFor(roomType => roomType.Description)
                .NotEmpty()
                .WithMessage("Room type description is required.");
            RuleFor(roomType => roomType.MaxOccupancy)
                .GreaterThan(0)
                .WithMessage("Max occupancy must be greater than 0.");
            RuleFor(roomType => roomType.PricePerNight)
                .GreaterThan(0)
                .WithMessage("Price per night must be greater than 0.");
        }
    }
}
