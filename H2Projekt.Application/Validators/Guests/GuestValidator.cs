using FluentValidation;
using H2Projekt.Application.Validators.Bookings;
using H2Projekt.Domain;

namespace H2Projekt.Application.Validators.Rooms
{
    public class GuestValidator : AbstractValidator<Guest>
    {
        public GuestValidator()
        {
            RuleFor(g => g.FirstName)
                .NotEmpty()
                .WithMessage("Fornavn er påkrævet.")
                .MaximumLength(50)
                .WithMessage("Fornavn må ikke overstige 50 tegn.");
            RuleFor(g => g.LastName)
                .NotEmpty()
                .WithMessage("Efternavn er påkrævet.")
                .MaximumLength(50)
                .WithMessage("Efternavn må ikke overstige 50 tegn.");
            RuleFor(g => g.Email)
                .NotEmpty()
                .WithMessage("Email er påkrævet.")
                .EmailAddress()
                .WithMessage("Email skal være i et gyldigt format.")
                .MaximumLength(100)
                .WithMessage("Email må ikke overstige 100 tegn.");
            RuleFor(g => g.Bookings)
                .ForEach(booking => booking.SetValidator(new BookingValidator()))
                .When(g => g.Bookings is not null && g.Bookings.Any());
        }
    }
}
