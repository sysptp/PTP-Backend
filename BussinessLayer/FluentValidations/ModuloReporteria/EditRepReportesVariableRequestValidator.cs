using BussinessLayer.DTOs.ModuloReporteria;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BussinessLayer.FluentValidations.ModuloGeneral.ModuloReporteria
{
    public class EditRepReportesVariableRequestValidator : AbstractValidator<EditRepReportesVariableDto>
    {
        public EditRepReportesVariableRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("El Id debe ser mayor que 0.");
            // NombreVariable: No puede ser nulo, vacío ni contener caracteres especiales.
            RuleFor(x => x.NombreVariable)
                .NotEmpty().WithMessage("NombreVariable es obligatorio.")
                .Must(SinCaracteresEspeciales).WithMessage("NombreVariable contiene caracteres inválidos.");

            // TipoVariable: No puede ser nulo o vacío.
            RuleFor(x => x.TipoVariable)
                .NotEmpty().WithMessage("TipoVariable es obligatorio.");

            // EsObligatorio: Se valida implícitamente porque es un booleano no nulo.

            // ValorPorDefecto: Es opcional, pero si está presente, no debe contener caracteres especiales.
            RuleFor(x => x.ValorPorDefecto)
                .Must(SinCaracteresEspeciales)
                .When(x => !string.IsNullOrWhiteSpace(x.ValorPorDefecto))
                .WithMessage("ValorPorDefecto contiene caracteres inválidos.");

            // Variable: No puede ser nulo, vacío ni contener caracteres especiales.
            RuleFor(x => x.Variable)
                .NotEmpty().WithMessage("Variable es obligatorio.")
                .Must(SinCaracteresEspeciales).WithMessage("Variable contiene caracteres inválidos.");
        }

        // Método auxiliar para validar que una cadena no contiene caracteres especiales.
        private bool SinCaracteresEspeciales(string input)
        {
            // Permitir letras, números, espacios y puntuación básica.
            var regex = new Regex(@"^[a-zA-Z0-9\s.,_-]*$");
            return regex.IsMatch(input);
        }
    }
}
