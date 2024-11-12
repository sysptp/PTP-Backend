using BussinessLayer.DTOs.ModuloInventario.Productos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloInventario.Productos
{
    public class EditProductsTypeRequestValidator : AbstractValidator<EditProductTypeDto>
    {

        public EditProductsTypeRequestValidator() {
            // Validation for Id
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID debe ser un número positivo y mayor a cero.");

            // Validation for NombreTipoProducto
            RuleFor(x => x.NombreTipoProducto)
                .NotEmpty().WithMessage("El nombre del tipo de producto no puede estar vacío.")
                .NotNull().WithMessage("El nombre del tipo de producto es obligatorio.")
                .Matches("^[a-zA-Z0-9 ]*$").WithMessage("El nombre del tipo de producto no debe contener caracteres especiales.");

            // Validation for Activo
            RuleFor(x => x.Activo)
                .NotNull().WithMessage("El campo Activo es obligatorio.");
        }
    }
}
