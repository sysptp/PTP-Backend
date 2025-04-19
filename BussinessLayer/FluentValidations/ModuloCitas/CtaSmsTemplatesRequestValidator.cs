using BussinessLayer.DTOs.ModuloCitas.CtaSmsTemplates;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCitas
{
    public class CtaSmsTemplatesRequestValidator : AbstractValidator<CtaSmsTemplatesRequest>
    {
        public CtaSmsTemplatesRequestValidator()
        {
            RuleFor(x => x.MessageContent)
                .NotEmpty().WithMessage("MessageContent es obligatorio.")
                .MaximumLength(255).WithMessage("MessageContent no debe exceder los 255 caracteres.");
        }
    }
}
