using BussinessLayer.DTOs.ModuloInventario.Versiones;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloInventario.Versiones
{
    public class EditVersionRequestValidation : AbstractValidator<EditVersionsDto>
    {

        public EditVersionRequestValidation() {
            // Validar que Nombre no sea nulo, no esté vacío y no contenga caracteres especiales peligrosos
            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("El Nombre no puede ser nulo.")
                .NotEmpty().WithMessage("El Nombre no puede estar vacío.")
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("El Nombre contiene caracteres no permitidos.")
                .MaximumLength(50).WithMessage("El Nombre no debe exceder los 50 caracteres.");

            // Validar que IdMarca no sea nulo y sea mayor que 0
            RuleFor(x => x.IdMarca)
                .NotNull().WithMessage("El Id de la marca no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la marca debe ser mayor que 0.");

            // Validar que IdMarca no sea nulo y sea mayor que 0
            RuleFor(x => x.Id)
                .NotNull().WithMessage("El Id no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id debe ser mayor que 0.");
        }
    }
}
