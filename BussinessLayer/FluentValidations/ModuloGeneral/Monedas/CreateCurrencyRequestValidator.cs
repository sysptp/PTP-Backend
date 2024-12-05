using BussinessLayer.DTOs.ModuloGeneral.Monedas;
using BussinessLayer.Interfaces.Repository.Empresa;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Monedas
{
    public class CreateCurrencyRequestValidator : AbstractValidator<CreateCurrencyDTO>
    {
        private readonly IGnEmpresaRepository _empresaRepository;

        public CreateCurrencyRequestValidator(
            IGnEmpresaRepository gnEmpresaRepository) {

            _empresaRepository = gnEmpresaRepository;

            // Validar que IdPais no sea nulo y sea mayor que 0
            RuleFor(x => x.IdPais)
                .NotNull().WithMessage("El Id del país no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del país debe ser mayor que 0.");

            // Validar que IdEmpresa no sea nulo y sea mayor que 0
            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la empresa debe ser mayor que 0.")
                .MustAsync(async (idEmpresa, cancellation) => await CompanyExits(idEmpresa))
                .WithMessage("La empresa especificada no existe.");

            // Validar que Descripcion no sea nula, no esté vacía y tenga un máximo de 100 caracteres
            RuleFor(x => x.Descripcion)
                .NotNull().WithMessage("La descripción no puede ser nula.")
                .NotEmpty().WithMessage("La descripción no puede estar vacía.")
                .MaximumLength(100).WithMessage("La descripción no puede exceder los 100 caracteres.");

            // Validar que Siglas no sea nula, no esté vacía y tenga un máximo de 10 caracteres
            RuleFor(x => x.Siglas)
                .NotNull().WithMessage("Las siglas no pueden ser nulas.")
                .NotEmpty().WithMessage("Las siglas no pueden estar vacías.")
                .MaximumLength(10).WithMessage("Las siglas no pueden exceder los 10 caracteres.");

            // Validar que Simbolo no sea nulo, no esté vacío y tenga un máximo de 5 caracteres
            RuleFor(x => x.Simbolo)
                .NotNull().WithMessage("El símbolo no puede ser nulo.")
                .NotEmpty().WithMessage("El símbolo no puede estar vacío.")
                .MaximumLength(5).WithMessage("El símbolo no puede exceder los 5 caracteres.");

            // Validar que EsLocal no sea nulo
            RuleFor(x => x.EsLocal)
                .NotNull().WithMessage("El campo EsLocal no puede ser nulo.");

            // Validar que TasaCambio no sea nula y sea mayor o igual a 0
            RuleFor(x => x.TasaCambio)
                .NotNull().WithMessage("La Tasa de Cambio no puede ser nula.")
                .GreaterThanOrEqualTo(0).WithMessage("La Tasa de Cambio debe ser mayor o igual a 0.");

        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }
    }
}
