using FluentValidation;

namespace H2Projekt.Application.Commands.Rooms.Validators
{
    public class UpdateRoomValidator : AbstractValidator<CreateRoomCommand>
    {
        public UpdateRoomValidator()
        {
            RuleFor(room => room.Number)
                .NotEmpty()
                .WithMessage("Værelsesnummer er påkrævet.")
                .Matches("[0-9]+.[0-9]+")
                .WithMessage("Værelsesnummer skal være i formatet 'X.Y' hvor X og Y er tal.");
            RuleFor(room => room.RoomTypeId)
                .GreaterThan(0)
                .WithMessage("Værelsestype ID'et skal være større end 0.");
            RuleFor(room => room.Status)
                .IsInEnum()
                .WithMessage("Status skal være en gyldig enum værdi.");
        }
    }
}
