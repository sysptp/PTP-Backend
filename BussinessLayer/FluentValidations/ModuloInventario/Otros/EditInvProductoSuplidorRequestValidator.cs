using BussinessLayer.DTOs.ModuloInventario.Otros;
using BussinessLayer.Interfaces.Services.ModuloInventario.Productos;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Otros
{
    public class EditInvProductoSuplidorRequestValidator : AbstractValidator<EditInvProductoSuplidorDTO>
    {
        private readonly IProductoService _productoService;
        private readonly ISuplidoresService _suplidoresService;
        private readonly IMonedasService _monedasService;

        public EditInvProductoSuplidorRequestValidator(IProductoService productoService,
            ISuplidoresService suplidoresService,
            IMonedasService monedasService)
        {

            _productoService = productoService;
            _suplidoresService = suplidoresService;
            _monedasService = monedasService;

            RuleFor(x => x.Id)
            .NotNull().WithMessage("El Id no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id debe ser mayor que 0.");

            // Validar que IdProducto no sea nulo y sea mayor que 0
            RuleFor(x => x.ProductoId)
                .NotNull().WithMessage("El Id del producto no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del producto debe ser mayor que 0.")
                .MustAsync(async (idProducto, cancellation) => await ProductExits(idProducto))
                .WithMessage("El producto especificada no existe.");

            RuleFor(x => x.SuplidorId)
                .NotNull().WithMessage("El Id del suplidor no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del suplidor debe ser mayor que 0.")
                .MustAsync(async (idSuplidor, cancellation) => await SupplierExits(idSuplidor))
                .WithMessage("El suplidor especificado no existe.");

            RuleFor(x => x.IdMoneda)
               .NotNull().WithMessage("El Id de la moneda no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la moneda debe ser mayor que 0.")
                .MustAsync(async (idMoneda, cancellation) => await CurrencyExits(idMoneda))
                .WithMessage("La moneda especificada no existe.");

            RuleFor(x => x.ValorCompra)
                .NotNull()
                .WithMessage("El ValorCompra no puede ser nulo.")
                .GreaterThan(0)
                .WithMessage("El ValorCompra debe ser mayor a 0.");

        }

        public async Task<bool> ProductExits(int productId)
        {
            var product = await _productoService.GetProductById(productId);
            return product != null;
        }

        public async Task<bool> SupplierExits(int supId)
        {
            var product = await _suplidoresService.GetById(supId);
            return product != null;
        }

        public async Task<bool> CurrencyExits(int id)
        {
            var product = await _monedasService.GetById(id);
            return product != null;
        }
    }
}
