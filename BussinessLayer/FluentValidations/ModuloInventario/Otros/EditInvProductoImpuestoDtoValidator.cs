using BussinessLayer.DTOs.ModuloInventario.Otros;
using BussinessLayer.Interfaces.ModuloInventario.Impuestos;
using BussinessLayer.Interfaces.ModuloInventario.Productos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloInventario.Otros
{
    public class EditInvProductoImpuestoDtoValidator : AbstractValidator<EditInvProductoImpuestoDto>
    {
        private readonly IProductoService _productoService;
        private readonly IImpuestosService _impuestosService;
        public EditInvProductoImpuestoDtoValidator(IProductoService productoService,
            IImpuestosService impuestosService
            )
        {
            _productoService = productoService;
            _impuestosService = impuestosService;

            RuleFor(x => x.Id)
                .NotNull().WithMessage("El ID es obligatorio.")
                .GreaterThan(0).WithMessage("El ID debe ser mayor a 0.");

            // Validar que IdProducto no sea nulo y sea mayor que 0
            RuleFor(x => x.ProductoId)
                .NotNull().WithMessage("El Id del producto no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del producto debe ser mayor que 0.")
                .MustAsync(async (idProducto, cancellation) => await ProductExits(idProducto))
                .WithMessage("El producto especificada no existe.");

            RuleFor(x => x.ImpuestoId)
                .NotNull().WithMessage("El Id del impuesto no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del impuesto debe ser mayor que 0.")
                .MustAsync(async (id, cancellation) => await TaxExits(id))
                .WithMessage("El impuesto especificado no existe.");
        }

        public async Task<bool> ProductExits(int productId)
        {
            var product = await _productoService.GetProductById(productId);
            return product != null;
        }

        public async Task<bool> TaxExits(int id)
        {
            var product = await _impuestosService.GetTaxById(id);
            return product != null;
        }
    }
}
