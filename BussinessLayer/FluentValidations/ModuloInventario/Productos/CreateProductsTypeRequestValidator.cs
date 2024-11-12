using BussinessLayer.DTOs.ModuloInventario.Productos;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Productos
{
    public class CreateProductsTypeRequestValidator : AbstractValidator<CreateTipoProductoDto>
    {

        public CreateProductsTypeRequestValidator() {

            // Validation for NombreTipoProducto
            RuleFor(x => x.NombreTipoProducto)
                .NotEmpty().WithMessage("El nombre del tipo de producto no puede estar vacío.")
                .NotNull().WithMessage("El nombre del tipo de producto es obligatorio.")
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("El nombre del tipo de producto no debe contener caracteres especiales.");

            // Validation for IdEmpresa
            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El ID de la empresa es obligatorio.")
                .GreaterThan(0).WithMessage("El ID de la empresa debe ser un número positivo.");
        }

    }
}
