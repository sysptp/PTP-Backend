using BussinessLayer.DTOs.Configuracion.Geografia.DProvincia;
using FluentValidation;

namespace BussinessLayer.FluentValidations.Configuracion.Geografia
{
    public class ProvinciaRequestValidator : AbstractValidator<ProvinceRequest>
    {
        public ProvinciaRequestValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("El nombre no puede ser null ni vacío");
        }
    }
}
