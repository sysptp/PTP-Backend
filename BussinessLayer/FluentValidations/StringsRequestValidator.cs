using FluentValidation;

namespace BussinessLayer.FluentValidations
{
    public class StringsRequestValidator : AbstractValidator<string>
    {
        public StringsRequestValidator()
        {
            // No permitir valores nulos
            RuleFor(x => x)
                .NotNull()
                .WithMessage("El campo no puede ser nulo.");

            // No permitir valores vacíos o espacios en blanco
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("El campo no puede estar vacío.")
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("El campo no puede contener solo espacios en blanco.");

            // Validar que solo contenga caracteres alfabéticos o numéricos (sin caracteres especiales)
            RuleFor(x => x)
                .Matches("^[a-zA-Z0-9]+$")
                .WithMessage("El campo solo debe contener numeros y letras, no se permiten caracteres especiales.");
        }
    }
}
