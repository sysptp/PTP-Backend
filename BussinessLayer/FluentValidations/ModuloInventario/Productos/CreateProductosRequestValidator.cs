using System.Text.RegularExpressions;
using BussinessLayer.DTOs.ModuloInventario.Productos;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Productos
{
    public class CreateProductosRequestValidator : AbstractValidator<CreateProductsDto>
    {
        public CreateProductosRequestValidator()
        {
            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("IdEmpresa cannot be null.")
                .Must(BeValidLong).WithMessage("IdEmpresa must be a valid long.");

            RuleFor(x => x.IdVersion)
                .NotNull().WithMessage("IdVersion cannot be null.")
                .Must(BeValidInt).WithMessage("IdVersion must be a valid integer.");

            RuleFor(x => x.IdTipoProducto)
                .NotNull().WithMessage("IdTipoProducto cannot be null.")
                .Must(BeValidInt).WithMessage("IdTipoProducto must be a valid integer.");

            RuleFor(x => x.CodigoBarra)
                .NotEmpty().WithMessage("CodigoBarra cannot be empty.")
                .Must(BeValidString).WithMessage("CodigoBarra must be a valid string without special characters.");

            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("Codigo cannot be empty.")
                .Must(BeValidString).WithMessage("Codigo must be a valid string without special characters.");

            RuleFor(x => x.NombreProducto)
                .NotEmpty().WithMessage("NombreProducto cannot be empty.")
                .Must(BeValidString).WithMessage("NombreProducto must be a valid string without special characters.");

            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("Descripcion cannot be empty.")
                .Must(BeValidString).WithMessage("Descripcion must be a valid string without special characters.");

            RuleFor(x => x.CantidadLote)
                .NotNull().WithMessage("CantidadLote cannot be null.")
                .Must(BeValidInt).WithMessage("CantidadLote must be a valid integer.");

            RuleFor(x => x.CantidadInventario)
                .NotNull().WithMessage("CantidadInventario cannot be null.")
                .Must(BeValidInt).WithMessage("CantidadInventario must be a valid integer.");

            RuleFor(x => x.CantidadMinima)
                .NotNull().WithMessage("CantidadMinima cannot be null.")
                .Must(BeValidInt).WithMessage("CantidadMinima must be a valid integer.");

            RuleFor(x => x.AdmiteDescuento)
                .NotNull().WithMessage("AdmiteDescuento cannot be null.")
                .Must(BeValidBool).WithMessage("AdmiteDescuento must be a valid boolean.");

            RuleFor(x => x.HabilitaVenta)
                .NotNull().WithMessage("HabilitaVenta cannot be null.")
                .Must(BeValidBool).WithMessage("HabilitaVenta must be a valid boolean.");

            RuleFor(x => x.AplicaImpuesto)
                .NotNull().WithMessage("AplicaImpuesto cannot be null.")
                .Must(BeValidBool).WithMessage("AplicaImpuesto must be a valid boolean.");

            RuleFor(x => x.EsLote)
                .NotNull().WithMessage("EsLote cannot be null.")
                .Must(BeValidBool).WithMessage("EsLote must be a valid boolean.");

            RuleFor(x => x.EsProducto)
                .NotNull().WithMessage("EsProducto cannot be null.")
                .Must(BeValidBool).WithMessage("EsProducto must be a valid boolean.");

            RuleFor(x => x.EsLocal)
                .NotNull().WithMessage("EsLocal cannot be null.")
                .Must(BeValidBool).WithMessage("EsLocal must be a valid boolean.");
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
