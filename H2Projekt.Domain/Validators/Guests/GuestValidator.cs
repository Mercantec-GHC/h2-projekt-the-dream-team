using FluentValidation;
using H2Projekt.Domain.Validators.Bookings;

namespace H2Projekt.Domain.Validators.Guests
{
    public class GuestValidator : AbstractValidator<Guest>
    {
        public GuestValidator()
        {
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
            RuleFor(guest => guest.PasswordHash)
                .NotEmpty()
                .WithMessage("Password hash er påkrævet.")
                .MinimumLength(64)
                .MaximumLength(64)
                .WithMessage("Password hash skal være 64 tegn langt (SHA-256 hash).")
                .When(guest => !string.IsNullOrEmpty(guest.PasswordHash));
            RuleFor(guest => guest.Bookings)
                .ForEach(booking => booking.SetValidator(new BookingValidator()))
                .When(guest => guest.Bookings is not null && guest.Bookings.Any());
        }
    }
}
