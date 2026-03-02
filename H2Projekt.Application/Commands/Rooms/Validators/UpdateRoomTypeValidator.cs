using FluentValidation;

namespace H2Projekt.Application.Commands.Rooms.Validators
{
    public class UpdateRoomTypeValidator : AbstractValidator<UpdateRoomTypeCommand>
    {
        public UpdateRoomTypeValidator()
        {
            RuleFor(request => request.Id)
                .GreaterThan(0)
                .WithMessage("Værelsestype ID'et skal være større end 0.");
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage("Navn er påkrævet.")
                .MaximumLength(50)
                .WithMessage("Navn må ikke overstige 50 tegn.");
            RuleFor(request => request.Description)
                .NotEmpty()
                .WithMessage("Beskrivelse er påkrævet.")
                .MaximumLength(200)
                .WithMessage("Beskrivelse må ikke overstige 200 tegn.");
            RuleFor(request => request.MaxOccupancy)
                .GreaterThan(0)
                .WithMessage("Maksimal belægning skal være større end 0.");
            RuleFor(request => request.PricePerNight)
                .GreaterThan(0)
                .WithMessage("Pris per nat skal være større end 0.");
        }
    }
}
