using BussinessLayer.DTOs.ModuloGeneral.Geografia.DPais;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Geografia
{
    public class CountryRequestValidator : AbstractValidator<CountryRequest>
    {
        public CountryRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre del país no puede ser vacío")
                .NotNull().WithMessage("El nombre del país no puede ser nulo");

        }
    }
}
