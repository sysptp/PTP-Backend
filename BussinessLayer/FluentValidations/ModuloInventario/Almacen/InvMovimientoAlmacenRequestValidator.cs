using FluentValidation;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using BussinessLayer.Interfaces.ModuloInventario.Almacen;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;

namespace BussinessLayer.FluentValidations.ModuloInventario.Almacen
{
    public class InvMovimientoAlmacenRequestValidator : AbstractValidator<InvMovimientoAlmacenRequest>
    {
        private readonly ISuplidoresService _suplidorRepository;
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly IInvAlmacenesRepository _almacenRepository;

        public InvMovimientoAlmacenRequestValidator(
            ISuplidoresService suplidorRepository,
            IGnEmpresaRepository empresaRepository,
            IInvAlmacenesRepository almacenRepository)
        {
            _suplidorRepository = suplidorRepository;
            _empresaRepository = empresaRepository;
            _almacenRepository = almacenRepository;

            RuleFor(x => x.IdSuplidor)
                .NotNull().WithMessage("El Id del suplidor (IdSuplidor) es obligatorio.")
                .MustAsync(async (id, cancellation) => await SuplidorExists(id))
                .WithMessage("El suplidor especificado no existe.");

            RuleFor(x => x.IdTipoMovimiento)
                .NotNull().WithMessage("El Id del tipo de movimiento (IdTipoMovimiento) es obligatorio.");

            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa (IdEmpresa) es obligatorio.")
                .MustAsync(async (id, cancellation) => await EmpresaExists(id))
                .WithMessage("La empresa especificada no existe.");

            RuleFor(x => x.IdAlmacen)
                .NotNull().WithMessage("El Id del almacén (IdAlmacen) es obligatorio.")
                .MustAsync(async (id, cancellation) => await AlmacenExists(id))
                .WithMessage("El almacén especificado no existe.");

            RuleFor(x => x.NoFactura)
                .NotEmpty().WithMessage("El número de factura (NoFactura) es obligatorio.")
                .MaximumLength(50).WithMessage("El número de factura no debe exceder los 50 caracteres.");

            RuleFor(x => x.CantidadProducto)
                .NotNull().WithMessage("La cantidad del producto (CantidadProducto) es obligatoria.")
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad del producto no puede ser negativa.");

            RuleFor(x => x.MontoInicial)
                .LessThan(0).WithMessage("El monto inicial no puede ser negativo.");

            RuleFor(x => x.MontoPendiente)
                .LessThan(0).WithMessage("El monto pendiente no puede ser negativo.");

            RuleFor(x => x.TotalFacturado)
                .GreaterThan(0).WithMessage("El total facturado debe ser mayor a cero.");

            RuleFor(x => x.EsCredito)
                .NotNull().WithMessage("El campo de crédito (EsCredito) es obligatorio.");
        }

        private async Task<bool> SuplidorExists(int? id)
        {
            if (!id.HasValue) return false;
            return await _suplidorRepository.GetById(id.Value) != null;
        }

        private async Task<bool> EmpresaExists(long? id)
        {
            if (!id.HasValue) return false;
            return await _empresaRepository.GetById(id.Value) != null;
        }

        private async Task<bool> AlmacenExists(int? id)
        {
            if (!id.HasValue) return false;
            return await _almacenRepository.GetById(id.Value) != null;
        }
    }
}
