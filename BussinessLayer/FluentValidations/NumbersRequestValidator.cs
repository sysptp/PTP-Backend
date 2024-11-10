using FluentValidation;

namespace BussinessLayer.FluentValidations
{
    public class NumbersRequestValidator : AbstractValidator<long>
    {
        public NumbersRequestValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("El valor no puede ser nulo.");

            RuleFor(x => x)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El valor debe ser un número positivo.");

        }
    }
}
