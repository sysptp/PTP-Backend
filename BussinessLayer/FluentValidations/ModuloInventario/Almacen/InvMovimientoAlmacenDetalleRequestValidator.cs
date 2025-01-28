using FluentValidation;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Interfaces.Services.ModuloInventario.Productos;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;

namespace BussinessLayer.FluentValidations.ModuloInventario.Almacen
{
    public class InvMovimientoAlmacenDetalleRequestValidator : AbstractValidator<InvMovimientoAlmacenDetalleRequest>
    {
        private readonly IProductoService _productoRepository;
        private readonly IGnEmpresaRepository _empresaRepository;

        public InvMovimientoAlmacenDetalleRequestValidator(
            IProductoService productoRepository,
            IGnEmpresaRepository empresaRepository)
        {
            _productoRepository = productoRepository;
            _empresaRepository = empresaRepository;

            RuleFor(x => x.IdMovimiento)
                .NotNull().WithMessage("El Id del movimiento (IdMovimiento) es obligatorio.");

            RuleFor(x => x.IdProducto)
                .NotNull().WithMessage("El Id del producto (IdProducto) es obligatorio.")
                .MustAsync(async (id, cancellation) => await ProductoExists(id))
                .WithMessage("El producto especificado no existe.");

            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa (IdEmpresa) es obligatorio.")
                .MustAsync(async (id, cancellation) => await EmpresaExists(id))
                .WithMessage("La empresa especificada no existe.");

            RuleFor(x => x.Cantidad)
                .NotNull().WithMessage("La cantidad (Cantidad) es obligatoria.")
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad no puede ser negativa.");

            RuleFor(x => x.EsVencimiento)
                .NotNull().WithMessage("El campo de vencimiento (EsVencimiento) es obligatorio.");

            RuleFor(x => x.FechaVencimiento)
                .GreaterThan(DateTime.Now).WithMessage("La fecha de vencimiento debe ser una fecha futura.")
                .When(x => x.EsVencimiento); 
        }

        private async Task<bool> ProductoExists(int? id)
        {
            if (!id.HasValue) return false;
            return await _productoRepository.GetProductById(id.Value) != null;
        }

        private async Task<bool> EmpresaExists(long? id)
        {
            if (!id.HasValue) return false;
            return await _empresaRepository.GetById(id.Value) != null;
        }
    }
}
