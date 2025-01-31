
using BussinessLayer.DTOs.ModuloGeneral.Utils;
using FluentValidation;

namespace BussinessLayer.Validations.ModuloGeneral.GnRepeatUnit
{
    public class GnRepeatUnitRequestValidation : AbstractValidator<GnRepeatUnitRequest>
    {
        public GnRepeatUnitRequestValidation()
        {
            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(80).WithMessage("La descripción no debe exceder los 80 caracteres.");
        }
    }
}