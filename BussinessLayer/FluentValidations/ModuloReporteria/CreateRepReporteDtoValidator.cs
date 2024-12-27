using BussinessLayer.DTOs.ModuloReporteria;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloReporteria
{
    public class CreateRepReporteDtoValidator : AbstractValidator<CreateRepReporteDto>
    {
        public CreateRepReporteDtoValidator()
        {

            // NombreReporte: Must not be null, empty or whitespace, and must not contain special characters.
            RuleFor(x => x.NombreReporte)
                .NotEmpty().WithMessage("NombreReporte es requerido.")
                .Must(NoSpecialCharacters).WithMessage("NombreReporte contains invalid characters.");

            // EsPesado: Must not be null.
            RuleFor(x => x.EsPesado)
                .NotNull().WithMessage("EsPesado es requerido.");

            // EsSubquery: Must not be null.
            RuleFor(x => x.EsSubquery)
                .NotNull().WithMessage("EsSubquery es requerido.");

            // DescripcionReporte: Must not be null or empty.
            RuleFor(x => x.DescripcionReporte)
                .NotEmpty().WithMessage("DescripcionReporte es requerido.");

            // QueryCommand: Must not be null, empty, or contain special characters.
            RuleFor(x => x.QueryCommand)
                .NotEmpty().WithMessage("QueryCommand es requerido.");

            // Activo: Must not be null.
            RuleFor(x => x.Activo)
                .NotNull().WithMessage("Activo es requerido.");
        }

        // Helper method to check for special characters.
        private bool NoSpecialCharacters(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            // Allow letters, numbers, spaces, and basic punctuation.
            var regex = new Regex(@"^[a-zA-Z0-9\s.,_-]*$");
            return regex.IsMatch(input);
        }
    }

}
