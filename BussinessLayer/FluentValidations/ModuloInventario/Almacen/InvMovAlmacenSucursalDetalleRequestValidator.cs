using FluentValidation;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Interfaces.Services.ModuloInventario.Productos;

namespace BussinessLayer.FluentValidations.ModuloInventario.Almacen
{
    public class InvMovAlmacenSucursalDetalleRequestValidator : AbstractValidator<InvMovAlmacenSucursalDetalleRequest>
    {
        private readonly IProductoService _productoRepository;

        public InvMovAlmacenSucursalDetalleRequestValidator(IProductoService productoRepository)
        {
            RuleFor(x => x.IdProducto)
               .NotNull().WithMessage("El Id del producto (IdProducto) es obligatorio.")
               .MustAsync(async (id, cancellation) => await _productoRepository.GetProductById(id) != null)
               .WithMessage("El producto especificado no existe.");

            RuleFor(x => x.CantidadProducto)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad del producto no puede ser negativa.");

            RuleFor(x => x.Activo)
                .NotNull().WithMessage("El campo Activo es obligatorio.");
            _productoRepository = productoRepository;
        }


    }
}
