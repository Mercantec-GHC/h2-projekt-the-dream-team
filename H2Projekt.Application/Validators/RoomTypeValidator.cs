using FluentValidation;
using H2Projekt.Domain;

namespace H2Projekt.Application.Validators
{
    public class RoomTypeValidator : AbstractValidator<RoomType>
    {
        public RoomTypeValidator()
        {
            RuleFor(roomType => roomType.Name).NotEmpty();
            RuleFor(roomType => roomType.Description).NotEmpty();
            RuleFor(roomType => roomType.MaxOccupancy).GreaterThan(0);
        }
    }
}
