using BussinessLayer.DTOs.ModuloInventario.Productos;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BussinessLayer.FluentValidations.ModuloInventario.Productos
{
    public class EditProductosRequestValidator : AbstractValidator<EditProductDto>
    {

        public EditProductosRequestValidator()
        {

            RuleFor(x => x.IdVersion)
    .NotNull().WithMessage("IdVersion no puede ser nulo.")
    .Must(BeValidInt).WithMessage("IdVersion debe ser un número entero válido.");

            RuleFor(x => x.IdTipoProducto)
                .NotNull().WithMessage("IdTipoProducto no puede ser nulo.")
                .Must(BeValidInt).WithMessage("IdTipoProducto debe ser un número entero válido.");

            RuleFor(x => x.CodigoBarra)
                .NotEmpty().WithMessage("CodigoBarra no puede estar vacío.")
                .Must(BeValidString).WithMessage("CodigoBarra debe ser una cadena válida sin caracteres especiales.");

            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("Codigo no puede estar vacío.")
                .Must(BeValidString).WithMessage("Codigo debe ser una cadena válida sin caracteres especiales.");

            RuleFor(x => x.NombreProducto)
                .NotEmpty().WithMessage("NombreProducto no puede estar vacío.")
                .Must(BeValidString).WithMessage("NombreProducto debe ser una cadena válida sin caracteres especiales.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("Descripcion no puede estar vacío.")
                .Must(BeValidString).WithMessage("Descripcion debe ser una cadena válida sin caracteres especiales.");

            RuleFor(x => x.CantidadLote)
                .NotNull().WithMessage("CantidadLote no puede ser nulo.")
                .Must(BeValidInt).WithMessage("CantidadLote debe ser un número entero válido.");

            RuleFor(x => x.CantidadMinima)
                .NotNull().WithMessage("CantidadMinima no puede ser nulo.")
                .Must(BeValidInt).WithMessage("CantidadMinima debe ser un número entero válido.");

            RuleFor(x => x.AdmiteDescuento)
                .NotNull().WithMessage("AdmiteDescuento no puede ser nulo.")
                .Must(BeValidBool).WithMessage("AdmiteDescuento debe ser un valor booleano válido.");

            RuleFor(x => x.HabilitaVenta)
                .NotNull().WithMessage("HabilitaVenta no puede ser nulo.")
                .Must(BeValidBool).WithMessage("HabilitaVenta debe ser un valor booleano válido.");

            RuleFor(x => x.AplicaImpuesto)
                .NotNull().WithMessage("AplicaImpuesto no puede ser nulo.")
                .Must(BeValidBool).WithMessage("AplicaImpuesto debe ser un valor booleano válido.");

            RuleFor(x => x.EsLote)
                .NotNull().WithMessage("EsLote no puede ser nulo.")
                .Must(BeValidBool).WithMessage("EsLote debe ser un valor booleano válido.");

            RuleFor(x => x.EsProducto)
                .NotNull().WithMessage("EsProducto no puede ser nulo.")
                .Must(BeValidBool).WithMessage("EsProducto debe ser un valor booleano válido.");

            RuleFor(x => x.EsLocal)
                .NotNull().WithMessage("EsLocal no puede ser nulo.")
                .Must(BeValidBool).WithMessage("EsLocal debe ser un valor booleano válido.");

        }

        private bool BeValidLong(long? value)
        {
            return !value.HasValue || value.Value.GetType() == typeof(long);
        }

        private bool BeValidInt(int? value)
        {
            return !value.HasValue || value.Value.GetType() == typeof(int);
        }

        private bool BeValidString(string? value)
        {
            // Check if the string is not null and does not contain special characters.
            return value != null && Regex.IsMatch(value, @"^[a-zA-Z0-9\s]+$");
        }

        private bool BeValidBool(bool? value)
        {
            return !value.HasValue || value.Value.GetType() == typeof(bool);
        }
    }
}
