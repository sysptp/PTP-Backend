using BussinessLayer.DTOs.HelpDesk;
using FluentValidation;

namespace BussinessLayer.FluentValidations.HelpDesk
{
    public class HdkStatusTicketRequestValidator:AbstractValidator<HdkStatusTicketRequest>
    {
        public HdkStatusTicketRequestValidator() { }
    }
}
