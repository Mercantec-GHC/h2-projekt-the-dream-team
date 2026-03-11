using FluentValidation;

namespace H2Projekt.Application.Commands.Authentication.Validators
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
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
