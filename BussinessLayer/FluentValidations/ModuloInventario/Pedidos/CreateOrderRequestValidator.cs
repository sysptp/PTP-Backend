using BussinessLayer.DTOs.ModuloInventario.Pedidos;
using BussinessLayer.Interfaces.Repository.Empresa;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Pedidos
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderDto>
    {
        private readonly IGnEmpresaRepository _empresaRepository;

        public CreateOrderRequestValidator(IGnEmpresaRepository gnEmpresaRepository) {

            _empresaRepository = gnEmpresaRepository;

            // Validar que IdEmpresa no sea nulo y sea mayor que 0
            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la empresa debe ser mayor que 0.")
                .MustAsync(async (idEmpresa, cancellation) => await CompanyExists(idEmpresa))
                .WithMessage("La empresa especificada no existe.");

            // Validar que IdSuplidor no sea nulo y sea mayor que 0
            RuleFor(x => x.IdSuplidor)
                .NotNull().WithMessage("El Id del suplidor no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del suplidor debe ser mayor que 0.");

            // Validar que Solicitado no sea nulo
            RuleFor(x => x.Solicitado)
                .NotNull().WithMessage("El campo Solicitado no puede ser nulo.");
        }

        private async Task<bool> CompanyExists(long companyId)
        {
            if (companyId <= 0) return false; 
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }
    }
}
