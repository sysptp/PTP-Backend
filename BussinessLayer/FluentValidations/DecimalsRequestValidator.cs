using FluentValidation;

namespace BussinessLayer.FluentValidations
{
    public class DecimalsRequestValidator : AbstractValidator<decimal>
    {
        public DecimalsRequestValidator()
        {
            // Validar que el valor no sea nulo
            RuleFor(x => x)
                .NotNull().WithMessage("El valor no puede ser nulo.");

            // Validar que el valor no sea igual a 0
            RuleFor(x => x)
                .Must(value => value != 0)
                .WithMessage("El valor no puede ser igual a 0.");
        }
    }
}
