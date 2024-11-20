using BussinessLayer.DTOs.ModuloInventario.Descuentos;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Descuentos
{
    public class EditDiscountRequestValidation : AbstractValidator<EditDiscountDto>
    {
        public EditDiscountRequestValidation() {

            // Validar que Id no sea nulo y sea mayor que 0 (si se requiere)
            RuleFor(x => x.Id)
                .NotNull().WithMessage("El Id no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id debe ser mayor que 0.");

            // Validar que IdProducto no sea nulo y sea mayor que 0
            RuleFor(x => x.IdProducto)
                .NotNull().WithMessage("El Id del producto no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del producto debe ser mayor que 0.");

            // Validar que EsPorcentaje no sea nulo
            RuleFor(x => x.EsPorcentaje)
                .NotNull().WithMessage("EsPorcentaje no puede ser nulo.");

            // Validar que ValorDescuento esté dentro de un rango permitido según si es porcentaje o monto fijo
            RuleFor(x => x.ValorDescuento)
                .NotNull().WithMessage("El ValorDescuento no puede ser nulo.")
                .GreaterThanOrEqualTo(0).WithMessage("El ValorDescuento debe ser mayor o igual a 0.")
                .When(x => x.EsPorcentaje == true)
                .LessThanOrEqualTo(100).WithMessage("El ValorDescuento no debe exceder 100 si es un porcentaje.")
                .When(x => x.EsPorcentaje == true);

            // Validar que EsPermanente no sea nulo
            RuleFor(x => x.EsPermanente)
                .NotNull().WithMessage("EsPermanente no puede ser nulo.");

            // Validar que Activo no sea nulo
            RuleFor(x => x.Activo)
                .NotNull().WithMessage("Activo no puede ser nulo.");
        }
    }
}
