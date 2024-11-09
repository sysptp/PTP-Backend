using BussinessLayer.DTOs.ModuloInventario.Precios;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Precios
{
    public class CreatePreciosRequestValidator : AbstractValidator<CreatePreciosDto>
    {
        public CreatePreciosRequestValidator()
        {
            // IdProducto: campo requerido
            RuleFor(x => x.IdProducto)
                .NotNull().WithMessage("IdProducto es requerido.")
                .Must(x => x is int).WithMessage("IdProducto debe ser un número entero.");

            // IdEmpresa: campo requerido
            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("IdEmpresa es requerido.")
                .Must(x => x is long).WithMessage("IdEmpresa debe ser un número de tipo long.");

            // IdMoneda: campo requerido
            RuleFor(x => x.IdMoneda)
                .NotNull().WithMessage("IdMoneda es requerido.")
                .Must(x => x is int).WithMessage("IdMoneda debe ser un número entero.");

            // PrecioValor: campo requerido y validación de rango positivo (opcional)
            RuleFor(x => x.PrecioValor)
                .NotNull().WithMessage("PrecioValor es requerido.")
                .Must(x => x is decimal).WithMessage("PrecioValor debe ser un número decimal.")
                .GreaterThan(0).WithMessage("PrecioValor debe ser mayor que cero.");

            // HabilitarVenta: campo requerido
            RuleFor(x => x.HabilitarVenta)
                .NotNull().WithMessage("HabilitarVenta es requerido.")
                .Must(x => x is bool).WithMessage("HabilitarVenta debe ser de tipo booleano.");
        }
    }
}
