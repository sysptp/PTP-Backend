using FluentValidation;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;

namespace BussinessLayer.FluentValidations.ModuloInventario.Almacen
{
    public class InvMovInventarioSucursalRequestValidator : AbstractValidator<InvMovInventarioSucursalRequest>
    {
        private readonly IGnSucursalRepository _gnSucursalRepository;

        public InvMovInventarioSucursalRequestValidator(IGnSucursalRepository gnSucursalRepository)
        {
            _gnSucursalRepository = gnSucursalRepository;

            RuleFor(x => x.IdSucursal)
                .NotNull().WithMessage("El Id de la sucursal (IdSucursal) es obligatorio.")
                .MustAsync(async (id, cancellation) => await SucursalExists(id))
                .WithMessage("La sucursal especificada no existe.");

            RuleFor(x => x.IdTransaccion)
                .NotNull().WithMessage("El Id de la transacción (IdTransaccion) es obligatorio.");

            RuleFor(x => x.IdModulo)
                .NotNull().WithMessage("El Id del módulo (IdModulo) es obligatorio.");

            RuleFor(x => x.IdTransaccionOrigen)
                .NotNull().WithMessage("El Id de la transacción origen (IdTransaccionOrigen) es obligatorio.");

            RuleFor(x => x.CantidadProducto)
                .NotNull().WithMessage("La cantidad del producto (CantidadProducto) es obligatoria.")
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad del producto no puede ser negativa.");

            RuleFor(x => x.Activo)
                .NotNull().WithMessage("El campo de estado activo (Activo) es obligatorio.");
        }

        private async Task<bool> SucursalExists(long id)
        {
            return await _gnSucursalRepository.GetById(id) != null;
        }
    }
}
