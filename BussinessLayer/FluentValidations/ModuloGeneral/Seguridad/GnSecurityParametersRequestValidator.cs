using BussinessLayer.DTOs.ModuloGeneral.Seguridad.GnSecurityParameters;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Seguridad
{
    public class GnSecurityParametersRequestValidator : AbstractValidator<GnSecurityParametersRequest>
    {
        public GnSecurityParametersRequestValidator()
        {

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId es obligatorio.");

        }
    }
}
