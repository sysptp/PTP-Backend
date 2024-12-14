using BussinessLayer.DTOs.ModuloInventario.Pedidos;
using BussinessLayer.Interfaces.ModuloInventario.Productos;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Pedidos
{
    public class EditDetallePedidoRequestValidator : AbstractValidator<EditDetallePedidoDto>
    {
        private readonly IProductoService _productoService;
        private readonly IPedidoService _pedidoService;

        public EditDetallePedidoRequestValidator(IProductoService productoService,
            IPedidoService pedidoService)
        {

            _productoService = productoService;
            _pedidoService = pedidoService;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El campo Id es obligatorio.")
                .GreaterThan(0).WithMessage("El Id debe ser mayor a 0.");

            RuleFor(x => x.IdProducto)
                .NotNull().WithMessage("El Id del producto no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del producto debe ser mayor que 0.")
                .MustAsync(async (idProducto, cancellation) => await ProductExits(idProducto))
                .WithMessage("El producto especificada no existe.");

            RuleFor(x => x.IdPedido)
               .NotNull().WithMessage("El Id del pedido no puede ser nulo.")
               .GreaterThan(0).WithMessage("El Id del pedido debe ser mayor que 0.")
               .MustAsync(async (id, cancellation) => await OrderExits(id))
               .WithMessage("El pedido especificado no existe.");

            RuleFor(x => x.Cantidad)
                .NotEmpty().WithMessage("El campo Cantidad es obligatorio.")
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");
        }

        public async Task<bool> ProductExits(int productId)
        {
            var product = await _productoService.GetProductById(productId);
            return product != null;
        }

        public async Task<bool> OrderExits(int id)
        {
            var company = await _pedidoService.GetById(id);
            return company != null;
        }
    }
}
