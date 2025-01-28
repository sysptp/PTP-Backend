using FluentValidation;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Interfaces.Services.ModuloInventario.Productos;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;

namespace BussinessLayer.FluentValidations.ModuloInventario.Almacen
{
    public class InvInventarioSucursalRequestValidator : AbstractValidator<InvInventarioSucursalRequest>
    {
        private readonly IProductoService _productoRepository;
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly IGnSucursalRepository _sucursalRepository;

        public InvInventarioSucursalRequestValidator(
            IProductoService productoRepository,
            IGnEmpresaRepository empresaRepository,
            IGnSucursalRepository sucursalRepository)
        {
            _productoRepository = productoRepository;
            _empresaRepository = empresaRepository;
            _sucursalRepository = sucursalRepository;

            RuleFor(x => x.IdProducto)
                .NotNull().WithMessage("El Id del producto (IdProducto) es obligatorio.")
                .MustAsync(async (id, cancellation) => await ProductoExists(id))
                .WithMessage("El producto especificado no existe.");

            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa (IdEmpresa) es obligatorio.")
                .MustAsync(async (id, cancellation) => await EmpresaExists(id))
                .WithMessage("La empresa especificada no existe.");

            RuleFor(x => x.IdSucursal)
                .NotNull().WithMessage("El Id de la sucursal (IdSucursal) es obligatorio.")
                .MustAsync(async (id, cancellation) => await SucursalExists(id))
                .WithMessage("La sucursal especificada no existe.");

            RuleFor(x => x.UbicacionExhibicion)
                .NotEmpty().WithMessage("La ubicación de exhibición es obligatoria.")
                .MaximumLength(500).WithMessage("La ubicación de exhibición no debe exceder los 500 caracteres.");

            RuleFor(x => x.UbicacionGuardada)
                .NotEmpty().WithMessage("La ubicación guardada es obligatoria.")
                .MaximumLength(500).WithMessage("La ubicación guardada no debe exceder los 500 caracteres.");

            RuleFor(x => x.CantidadProducto)
                .NotNull().WithMessage("La cantidad del producto (CantidadProducto) es obligatoria.")
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad del producto no puede ser negativa.");

            RuleFor(x => x.CantidadMinima)
                .NotNull().WithMessage("La cantidad mínima (CantidadMinima) es obligatoria.")
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad mínima no puede ser negativa.");

            RuleFor(x => x.Activo)
                .NotNull().WithMessage("El estado activo es obligatorio.");
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

        private async Task<bool> SucursalExists(long? id)
         {
            if (!id.HasValue) return false;
            return await _sucursalRepository.GetById(id.Value) != null;
        }
    }
}
