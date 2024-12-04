using BussinessLayer.DTOs.HelpDesk;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloHelpDesk
{
    public class HdkStatusTicketRequestValidator : AbstractValidator<HdkStatusTicketRequest>
    {
        public HdkStatusTicketRequestValidator() { }
    }
}
