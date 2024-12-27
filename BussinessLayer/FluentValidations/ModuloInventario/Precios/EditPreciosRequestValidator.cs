using BussinessLayer.DTOs.ModuloInventario.Precios;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using BussinessLayer.Interfaces.Services.ModuloInventario.Productos;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Precios
{
    public class EditPreciosRequestValidator : AbstractValidator<EditPricesDto>
    {
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly IProductoService _productoService;
        public EditPreciosRequestValidator(IGnEmpresaRepository gnEmpresaRepository,
            IProductoService productoService)
        {

            _empresaRepository = gnEmpresaRepository;
            _productoService = productoService;

            // Validar que Id sea mayor que 0
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El Id debe ser mayor que 0.");
            // Validar que IdProducto no sea nulo y sea mayor que 0
            RuleFor(x => x.IdProducto)
                .NotNull().WithMessage("El Id del producto no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del producto debe ser mayor que 0.")
                .MustAsync(async (idProducto, cancellation) => await ProductExits(idProducto))
                .WithMessage("El producto especificada no existe.");

            // IdMoneda: campo requerido
            RuleFor(x => x.IdMoneda)
                .NotNull().WithMessage("IdMoneda es requerido.")
                .Must(x => x is int).WithMessage("IdMoneda debe ser un número entero.");

            // PrecioValor: campo requerido y validación de rango positivo (opcional)
            RuleFor(x => x.PrecioValor)
                .NotNull().WithMessage("PrecioValor es requerido.")
                .Must(x => x is decimal).WithMessage("PrecioValor debe ser un número decimal.")
                .GreaterThan(0).WithMessage("PrecioValor debe ser mayor que cero.");

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
