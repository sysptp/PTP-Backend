using BussinessLayer.DTOs.ModuloInventario.Pedidos;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloInventario.Pedidos
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderDto>
    {

        public CreateOrderRequestValidator() {

            // Validar que IdEmpresa no sea nulo y sea mayor que 0
            RuleFor(x => x.IdEmpresa)
                .NotNull().WithMessage("El Id de la empresa no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id de la empresa debe ser mayor que 0.");

            // Validar que IdSuplidor no sea nulo y sea mayor que 0
            RuleFor(x => x.IdSuplidor)
                .NotNull().WithMessage("El Id del suplidor no puede ser nulo.")
                .GreaterThan(0).WithMessage("El Id del suplidor debe ser mayor que 0.");

            // Validar que Solicitado no sea nulo
            RuleFor(x => x.Solicitado)
                .NotNull().WithMessage("El campo Solicitado no puede ser nulo.");
        }
    }
}
