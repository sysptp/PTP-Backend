using FluentValidation;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Interfaces.Services.ModuloInventario.Productos;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;

namespace BussinessLayer.FluentValidations.ModuloInventario.Almacen
{
    public class InvMovimientoSucursalDetalleRequestValidator : AbstractValidator<InvMovimientoSucursalDetalleRequest>
    {
        private readonly IProductoService _productoRepository;
        private readonly IInvMovInventarioSucursalRepository _movInventarioSucursalRepository;

        public InvMovimientoSucursalDetalleRequestValidator(
            IProductoService productoRepository,
            IInvMovInventarioSucursalRepository movInventarioSucursalRepository)
        {
            _productoRepository = productoRepository;
            _movInventarioSucursalRepository = movInventarioSucursalRepository;

            RuleFor(x => x.IdProducto)
                .NotNull().WithMessage("El Id del producto (IdProducto) es obligatorio.")
                .MustAsync(async (id, cancellation) => await ProductoExists(id))
                .WithMessage("El producto especificado no existe.");

            RuleFor(x => x.IdMovInventarioSucursal)
                .NotNull().WithMessage("El Id del movimiento de inventario sucursal (IdMovInventarioSucursal) es obligatorio.")
                .MustAsync(async (id, cancellation) => await MovInventarioSucursalExists(id))
                .WithMessage("El movimiento de inventario sucursal especificado no existe.");

            RuleFor(x => x.CantidadProducto)
                .NotNull().WithMessage("La cantidad del producto (CantidadProducto) es obligatoria.")
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad del producto no puede ser negativa.");

            RuleFor(x => x.Activo)
                .NotNull().WithMessage("El campo de estado activo (Activo) es obligatorio.");
        }

        private async Task<bool> ProductoExists(int? id)
        {
            if (!id.HasValue) return false;
            return await _productoRepository.GetProductById(id.Value) != null;
        }

        private async Task<bool> MovInventarioSucursalExists(int? id)
        {
            if (!id.HasValue) return false;
            return await _movInventarioSucursalRepository.GetById(id.Value) != null;
        }
    }
}
