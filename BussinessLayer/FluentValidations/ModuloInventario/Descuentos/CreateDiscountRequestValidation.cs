using BussinessLayer.DTOs.ModuloInventario.Descuentos;
using BussinessLayer.Interfaces.ModuloInventario.Productos;
using BussinessLayer.Interfaces.Repository.Empresa;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Descuentos
{
    public class CreateDiscountRequestValidation : AbstractValidator<CreateDiscountDto>
    {
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly IProductoService _productoService;

        public CreateDiscountRequestValidation(IGnEmpresaRepository gnEmpresaRepository, 
            IProductoService productoService) {

            _empresaRepository = gnEmpresaRepository;
            _productoService = productoService;

            // Validar que IdProducto no sea nulo y sea mayor que 0
            RuleFor(x => x.IdProducto)
                .NotNull().WithMessage("El Id del producto no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del producto debe ser mayor que 0.")
                .MustAsync(async (idProducto, cancellation) => await ProductExits(idProducto))
                .WithMessage("El producto especificada no existe.");

            // Validar que IdEmpresa no sea nulo y sea mayor que 0
            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la empresa debe ser mayor que 0.")
                .MustAsync(async (idEmpresa, cancellation) => await CompanyExits(idEmpresa))
                .WithMessage("La empresa especificada no existe.");

            // Validar que EsPorcentaje no sea nulo
            RuleFor(x => x.EsPorcentaje)
                .NotNull().WithMessage("EsPorcentaje no puede ser nulo.");

            // Validar que ValorDescuento esté dentro de un rango permitido según si es porcentaje o monto fijo
            RuleFor(x => x.ValorDescuento)
                .NotNull().WithMessage("El ValorDescuento no puede ser nulo.")
                .GreaterThanOrEqualTo(0).WithMessage("El ValorDescuento debe ser mayor o igual a 0.")
                .When(x => x.EsPorcentaje == true)
                .LessThanOrEqualTo(100).WithMessage("El ValorDescuento no debe exceder 100 si es un porcentaje.")
                .When(x => x.EsPorcentaje == true);

            // Validar que EsPermanente no sea nulo
            RuleFor(x => x.EsPermanente)
                .NotNull().WithMessage("EsPermanente no puede ser nulo.");

            // Validar que Activo no sea nulo
            RuleFor(x => x.Activo)
                .NotNull().WithMessage("Activo no puede ser nulo.");
        }

        public async Task<bool> CompanyExits(long companyId)
        {
            var company = await _empresaRepository.GetById(companyId);
            return company != null;
        }

        public async Task<bool> ProductExits(int productId)
        {
            var product = await _productoService.GetProductById(productId);
            return product != null;
        }
    }
}
