using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Geografia.DPais;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Configuracion.Geografia
{
    public class PaisRequestValidator : AbstractValidator<CountryRequest>
    {
        public PaisRequestValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("El nombre no puede ser null ni vacío");
        }
    }

}
