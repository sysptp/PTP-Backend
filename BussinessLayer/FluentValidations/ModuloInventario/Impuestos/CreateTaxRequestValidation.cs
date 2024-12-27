using BussinessLayer.DTOs.ModuloInventario.Impuestos;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Impuestos
{
    public class CreateTaxRequestValidation : AbstractValidator<CreateTaxDto>
    {
        private readonly IGnEmpresaRepository _empresaRepository;
        public CreateTaxRequestValidation(IGnEmpresaRepository gnEmpresaRepository) {

            _empresaRepository = gnEmpresaRepository;

            // Validar que IdEmpresa no sea nulo y sea mayor que 0
            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la empresa debe ser mayor que 0.")
                .MustAsync(async (idEmpresa, cancellation) => await CompanyExits(idEmpresa))
                .WithMessage("La empresa especificada no existe.");

            // Validar que IdMoneda no sea nulo y sea mayor que 0
            RuleFor(x => x.IdMoneda)
                .NotNull().WithMessage("El Id de la moneda no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la moneda debe ser mayor que 0.");

            // Validar que EsPorcentaje no sea nulo
            RuleFor(x => x.EsPorcentaje)
                .NotNull().WithMessage("EsPorcentaje no puede ser nulo.");

            // Validar que ValorImpuesto esté dentro de un rango permitido (e.g., 0 a 100 si es porcentaje)
            RuleFor(x => x.ValorImpuesto)
                .GreaterThanOrEqualTo(0).WithMessage("El ValorImpuesto debe ser mayor o igual a 0.")
                .When(x => x.EsPorcentaje == true)
                .LessThanOrEqualTo(100).WithMessage("El ValorImpuesto no debe exceder 100 si es un porcentaje.")
                .When(x => x.EsPorcentaje == true);

            // Validar que NombreImpuesto no sea nulo, no esté vacío y no contenga caracteres especiales peligrosos
            RuleFor(x => x.NombreImpuesto)
                .NotNull().WithMessage("El NombreImpuesto no puede ser nulo.")
                .NotEmpty().WithMessage("El NombreImpuesto no puede estar vacío.")
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("El NombreImpuesto contiene caracteres no permitidos.")
                .MaximumLength(50).WithMessage("El NombreImpuesto no debe exceder los 50 caracteres.");
        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }

    }
}
