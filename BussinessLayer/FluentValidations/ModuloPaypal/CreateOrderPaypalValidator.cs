using BussinessLayer.DTOs.ModuloPaypal;
using BussinessLayer.DTOs.Paypal.CreateOrder;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloPaypal
{
    public class CreateOrderPaypalValidator : AbstractValidator<PaypalOrderCreateDto>
    {
        public CreateOrderPaypalValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description no puede ser vacio").NotNull().WithMessage("Description no puede ser nulo");

            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount no puede ser vacio").NotNull().WithMessage("Amount no puede ser nulo")
                .GreaterThanOrEqualTo(1).WithMessage("Amount debe ser mayor a uno");
                
        }
    }
}
