using BussinessLayer.DTOs.ModuloInventario.Otros;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloInventario.Otros
{
    public class EditTipoMovimientoRequestValidation : AbstractValidator<EditTipoMovimientoDto>
    {
        public EditTipoMovimientoRequestValidation()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.")
                .Matches("^[a-zA-Z0-9 ]+$").WithMessage("El nombre solo puede contener caracteres alfanuméricos y espacios.");

            RuleFor(x => x.IN_OUT)
                .NotNull().WithMessage("Debe especificar si es IN o OUT.");
        }
    }
}
