using BussinessLayer.DTOs.ModuloInventario.Productos;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Repository.REmpresa;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Productos
{
    public class CreateProductsTypeRequestValidator : AbstractValidator<CreateTipoProductoDto>
    {
        private readonly IGnEmpresaRepository _empresaRepository;

        public CreateProductsTypeRequestValidator(IGnEmpresaRepository gnEmpresaRepository) {

            _empresaRepository = gnEmpresaRepository;

            // Validation for NombreTipoProducto
            RuleFor(x => x.NombreTipoProducto)
                .NotEmpty().WithMessage("El nombre del tipo de producto no puede estar vacío.")
                .NotNull().WithMessage("El nombre del tipo de producto es obligatorio.")
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("El nombre del tipo de producto no debe contener caracteres especiales.");

            // Validar que IdEmpresa no sea nulo y sea mayor que 0
            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la empresa debe ser mayor que 0.")
                .MustAsync(async (idEmpresa, cancellation) => await CompanyExits(idEmpresa))
                .WithMessage("La empresa especificada no existe.");
        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }

    }
}
