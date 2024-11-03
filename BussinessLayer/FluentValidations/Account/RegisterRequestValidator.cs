using BussinessLayer.Dtos.Account;
using FluentValidation;

namespace BussinessLayer.FluentValidations.Account
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("El nombre es requerido.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es requerido.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es requerido.")
                .EmailAddress().WithMessage("La dirección de correo electrónico no es válida.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("El nombre de usuario es requerido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .When(x => !string.IsNullOrEmpty(x.Password))
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.")
                .Matches("[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula.")
                .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un dígito.")
                .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("La confirmación de la contraseña debe coincidir con la contraseña.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("El número de teléfono es requerido.");
        }
    }
}
