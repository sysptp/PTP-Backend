using FluentValidation;
using BussinessLayer.DTOs.Account;

namespace YourProject.Validators
{
    public class TwoFactorVerificationRequestValidator : AbstractValidator<TwoFactorVerificationRequest>
    {
        public TwoFactorVerificationRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("El ID de usuario es obligatorio");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("El código de verificación es obligatorio")
                .Length(6).WithMessage("El código debe tener exactamente 6 caracteres");
        }
    }
}