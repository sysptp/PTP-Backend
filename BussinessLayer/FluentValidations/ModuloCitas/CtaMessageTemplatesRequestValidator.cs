using BussinessLayer.DTOs.ModuloCitas.CtaMessageTemplates;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCitas
{
    public class CtaMessageTemplatesRequestValidator : AbstractValidator<CtaMessageTemplatesRequest>
    {
        public CtaMessageTemplatesRequestValidator()
        {
            RuleFor(x => x.MessageContent)
                .NotEmpty().WithMessage("MessageContent es obligatorio.")
                .MaximumLength(255).WithMessage("MessageContent no debe exceder los 255 caracteres.");
            
        }
    }
}
