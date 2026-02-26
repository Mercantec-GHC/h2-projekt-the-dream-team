using FluentValidation;

namespace H2Projekt.Domain.Validators.Rooms
{
    public class RoomTypeValidator : AbstractValidator<RoomType>
    {
        public RoomTypeValidator()
        {
            RuleFor(roomType => roomType.Name)
                .NotEmpty()
                .WithMessage("Navn er påkrævet.")
                .MaximumLength(50)
                .WithMessage("Navn må ikke overstige 50 tegn.");
            RuleFor(roomType => roomType.Description)
                .NotEmpty()
                .WithMessage("Beskrivelse er påkrævet.")
                .MaximumLength(200)
                .WithMessage("Beskrivelse må ikke overstige 200 tegn.");
            RuleFor(roomType => roomType.MaxOccupancy)
                .GreaterThan(0)
                .WithMessage("Maksimal belægning skal være større end 0.");
            RuleFor(roomType => roomType.PetsAllowed)
                .NotNull()
                .WithMessage("Angiv venligst om kæledyr er tilladt.");
            RuleFor(roomType => roomType.PricePerNight)
                .GreaterThan(0)
                .WithMessage("Pris per nat skal være større end 0.");
            RuleFor(roomType => roomType.Rooms)
                .ForEach(room => room.SetValidator(new RoomValidator()))
                .When(roomType => roomType.Rooms is not null && roomType.Rooms.Any());
            RuleFor(roomType => roomType.RoomDiscounts)
                .ForEach(roomDiscount => roomDiscount.SetValidator(new RoomDiscountValidator()))
                .When(roomType => roomType.RoomDiscounts is not null && roomType.RoomDiscounts.Any());
        }
    }
}
