using FluentValidation;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;
using BussinessLayer.Interfaces.Services.ModuloInventario.Otros;

namespace BussinessLayer.FluentValidations.ModuloInventario.Almacen
{
    public class InvMovAlmacenSucursalRequestValidator : AbstractValidator<InvMovAlmacenSucursalRequest>
    {
        private readonly IInvAlmacenesRepository _almacenRepository;
        private readonly IGnSucursalRepository _sucursalRepository;
        private readonly ITipoMovimientoService _tipoMovimientoRepository;

        public InvMovAlmacenSucursalRequestValidator(IInvAlmacenesRepository almacenRepository, IGnSucursalRepository sucursalRepository, ITipoMovimientoService tipoMovimientoRepository)
        {
            _almacenRepository = almacenRepository;
            _sucursalRepository = sucursalRepository;
            _tipoMovimientoRepository = tipoMovimientoRepository;


            RuleFor(x => x.IdAlmacen)
                .NotNull().WithMessage("El Id del almacén (IdAlmacen) es obligatorio.")
                .MustAsync(async (id, cancellation) => await AlmacenExists(id))
                .WithMessage("El almacén especificado no existe.");

            RuleFor(x => x.IdSucursal)
                .NotNull().WithMessage("El Id de la sucursal (IdSucursal) es obligatorio.")
                .MustAsync(async (id, cancellation) => await SucursalExists(id))
                .WithMessage("La sucursal especificada no existe.");

            RuleFor(x => x.IdTipoMovimiento)
                .NotNull().WithMessage("El Id del tipo de movimiento (IdTipoMovimiento) es obligatorio.")
                .MustAsync(async (id, cancellation) => await TipoMovimientoExists(id))
                .WithMessage("El tipo de movimiento especificado no existe.");

            RuleFor(x => x.CantidadProducto)
                .NotNull().WithMessage("La cantidad del producto (CantidadProducto) es obligatoria.")
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad del producto no puede ser negativa.");

            RuleFor(x => x.Motivo)
                .MaximumLength(100).WithMessage("El motivo no debe exceder los 100 caracteres.");

            RuleFor(x => x.Activo)
                .NotNull().WithMessage("El estado activo es obligatorio.");
        }

        private async Task<bool> AlmacenExists(int? id)
        {
            if (!id.HasValue) return false;
            return await _almacenRepository.GetById(id.Value) != null;
        }

        private async Task<bool> SucursalExists(long? id)
        {
            if (!id.HasValue) return false;
            return await _sucursalRepository.GetById(id.Value) != null;
        }

        private async Task<bool> TipoMovimientoExists(int? id)
        {
            if (!id.HasValue) return false;
            return await _tipoMovimientoRepository.GetById(id.Value) != null;
        }
    
    }
}
