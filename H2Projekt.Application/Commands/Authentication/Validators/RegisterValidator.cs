using FluentValidation;

namespace H2Projekt.Application.Commands.Authentication.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(request => request.FirstName)
                .NotEmpty()
                .WithMessage("Fornavn er påkrævet.")
                .MaximumLength(50)
                .WithMessage("Fornavn må ikke overstige 50 tegn.");
            RuleFor(request => request.LastName)
                .NotEmpty()
                .WithMessage("Efternavn er påkrævet.")
                .MaximumLength(50)
                .WithMessage("Efternavn må ikke overstige 50 tegn.");
            RuleFor(request => request.Email)
                .NotEmpty()
                .WithMessage("Email er påkrævet.")
                .EmailAddress()
                .WithMessage("Email skal være i et gyldigt format.")
                .MaximumLength(100)
                .WithMessage("Email må ikke overstige 100 tegn.");
            RuleFor(request => request.Password)
                .NotEmpty()
                .WithMessage("Password er påkrævet.")
                .MinimumLength(8)
                .WithMessage("Password skal være mindst 8 tegn langt.")
                .MaximumLength(100)
                .WithMessage("Password må ikke overstige 100 tegn.");
        }
    }
}
