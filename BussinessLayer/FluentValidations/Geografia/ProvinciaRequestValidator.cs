

namespace BussinessLayer.FluentValidations.Geografia
{
    public class ProvinciaRequestValidator : AbstractValidator<MunicipioRequest>
    {
        public MunicipioRequestValidator()
        {

            RuleFor(x => x.Nombre)
                .NotEmpty().NotNull().WithMessage("El nombre no puede ser null ni vacío");
        }
    }
}
