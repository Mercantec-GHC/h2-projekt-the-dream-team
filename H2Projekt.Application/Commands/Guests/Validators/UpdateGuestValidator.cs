using FluentValidation;

namespace H2Projekt.Application.Commands.Guests.Validators
{
    public class UpdateGuestValidator : AbstractValidator<UpdateGuestCommand>
    {
        public UpdateGuestValidator()
        {
            RuleFor(guest => guest.Id)
                .GreaterThan(0)
                .WithMessage("Gæste ID'et skal være større end 0.");
            RuleFor(guest => guest.FirstName)
                .NotEmpty()
                .WithMessage("Fornavn er påkrævet.")
                .MaximumLength(50)
                .WithMessage("Fornavn må ikke overstige 50 tegn.");
            RuleFor(guest => guest.LastName)
                .NotEmpty()
                .WithMessage("Efternavn er påkrævet.")
                .MaximumLength(50)
                .WithMessage("Efternavn må ikke overstige 50 tegn.");
            RuleFor(guest => guest.Email)
                .NotEmpty()
                .WithMessage("Email er påkrævet.")
                .EmailAddress()
                .WithMessage("Email skal være i et gyldigt format.")
                .MaximumLength(100)
                .WithMessage("Email må ikke overstige 100 tegn.");
        }
    }
}
