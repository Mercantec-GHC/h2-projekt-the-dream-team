using FluentValidation;
using H2Projekt.Domain;

namespace H2Projekt.Application.Validators
{
    public class RoomValidator : AbstractValidator<Room>
    {
        public RoomValidator()
        {
            RuleFor(room => room.Number).NotEmpty().Matches("[0-9]+.[0-9]+");
            RuleFor(room => room.Capacity).GreaterThan(0);
            RuleFor(room => room.PricePerNight).GreaterThan(0);
            RuleFor(room => room.Status).IsInEnum();
        }
    }
}
