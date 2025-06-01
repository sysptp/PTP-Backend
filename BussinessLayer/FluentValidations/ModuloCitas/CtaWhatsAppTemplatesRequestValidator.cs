using BussinessLayer.DTOs.ModuloCitas.CtaWhatsAppTemplates;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCitas
{
    public class CtaWhatsAppTemplatesRequestValidator : AbstractValidator<CtaWhatsAppTemplatesRequest>
    {
        public CtaWhatsAppTemplatesRequestValidator()
        {
            RuleFor(x => x.MessageContent)
                .NotEmpty().WithMessage("MessageContent es obligatorio.")
                .MaximumLength(255).WithMessage("MessageContent no debe exceder los 255 caracteres.");
        }
    }
}
