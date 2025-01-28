using FluentValidation;
using BussinessLayer.DTOs.ModuloInventario.Almacenes;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using BussinessLayer.Interfaces.Services.ModuloInventario.Productos;
using BussinessLayer.Interfaces.Repository.Inventario.Almacenes;

namespace BussinessLayer.FluentValidations.ModuloInventario.Almacen
{
    public class InvAlmacenInventarioRequestValidator : AbstractValidator<InvAlmacenInventarioRequest>
    {
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly  IProductoService _productoService;
        private readonly IInvAlmacenesRepository _almacenRepository;

        public InvAlmacenInventarioRequestValidator(
            IGnEmpresaRepository empresaRepository,
            IProductoService productoService,
            IInvAlmacenesRepository almacenRepository)
        {
            _empresaRepository = empresaRepository;
            _productoService = productoService;
            _almacenRepository = almacenRepository;

            RuleFor(x => x.IdProducto)
                .GreaterThan(0).WithMessage("El Id del producto (IdProducto) es obligatorio.")
                .MustAsync(async (id, cancellation) => await _productoService.GetProductById(id) != null)
                .WithMessage("El Id del producto (IdProducto) no existe en la base de datos.");

            RuleFor(x => x.IdEmpresa)
                .GreaterThan(0).WithMessage("El Id de la empresa (IdEmpresa) es obligatorio.")
                .MustAsync(async (id, cancellation) => await _empresaRepository.GetById(id) != null)
                .WithMessage("El Id de la empresa (IdEmpresa) no existe en la base de datos.");

            RuleFor(x => x.IdAlmacen)
                .GreaterThan(0).WithMessage("El Id del almacén (IdAlmacen) es obligatorio.")
                .MustAsync(async (id, cancellation) => await _almacenRepository.GetById(id) != null)
                .WithMessage("El Id del almacén (IdAlmacen) no existe en la base de datos.");

            RuleFor(x => x.CantidadProducto)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad del producto (CantidadProducto) debe ser mayor o igual a 0.");

            RuleFor(x => x.CantidadMinima)
                .GreaterThanOrEqualTo(0).WithMessage("La cantidad mínima (CantidadMinima) debe ser mayor o igual a 0.");

            RuleFor(x => x.UbicacionExhibicion)
                .MaximumLength(500).WithMessage("La ubicación de exhibición no debe exceder los 500 caracteres.");

            RuleFor(x => x.UbicacionGuardada)
                .MaximumLength(500).WithMessage("La ubicación guardada no debe exceder los 500 caracteres.");

            RuleFor(x => x.Activo)
                .NotNull().WithMessage("Debe especificar si el inventario está activo o no.");
        }
    }
}
