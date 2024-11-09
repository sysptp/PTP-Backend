using FluentValidation;
using BussinessLayer.DTOs.ModuloGeneral.Sucursal;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Empresas
{
    public class GnSucursalRequestValidator : AbstractValidator<GnSucursalRequest>
    {
        public GnSucursalRequestValidator()
        {
            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("El ID de la empresa es requerido y debe ser mayor que cero.");

            RuleFor(x => x.SucursalName)
                .NotEmpty().WithMessage("El nombre de la sucursal es requerido.")
                .Length(1, 100).WithMessage("El nombre de la sucursal no debe exceder los 100 caracteres.");

            RuleFor(x => x.Phone)
                .Length(0, 15).WithMessage("El teléfono no debe exceder los 15 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.ResponsibleUserId)
                .GreaterThan(0).WithMessage("El ID del usuario responsable debe ser mayor que cero.")
                .When(x => x.ResponsibleUserId.HasValue);

            RuleFor(x => x.CountryId)
                .GreaterThan(0).WithMessage("El ID del país es requerido y debe ser mayor que cero.");

            RuleFor(x => x.RegionId)
                .GreaterThan(0).WithMessage("El ID de la región es requerido y debe ser mayor que cero.");

            RuleFor(x => x.ProvinceId)
                .GreaterThan(0).WithMessage("El ID de la provincia es requerido y debe ser mayor que cero.");

            RuleFor(x => x.MunicipalityId)
                .GreaterThan(0).WithMessage("El ID del municipio es requerido y debe ser mayor que cero.");

            RuleFor(x => x.Address)
                .Length(0, 250).WithMessage("La dirección no debe exceder los 250 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.IsPrincipal)
                .NotNull().WithMessage("Debe especificar si la sucursal es principal.");
        }
    }
}
