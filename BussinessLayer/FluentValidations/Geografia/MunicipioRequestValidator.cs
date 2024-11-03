using BussinessLayer.DTOs.Geografia.DMunicipio;
using FluentValidation;

namespace BussinessLayer.FluentValidations.Geografia
{
    public class MunicipioRequestValidator : AbstractValidator<MunicipioRequest>
    {
        public MunicipioRequestValidator() {

            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("El nombre no puede ser null ni vacío");
        }
    }
}
