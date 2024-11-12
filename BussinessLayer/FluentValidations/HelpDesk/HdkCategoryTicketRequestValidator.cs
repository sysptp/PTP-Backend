using FluentValidation;
using BussinessLayer.DTOs.HelpDesk;

namespace BussinessLayer.FluentValidations.HelpDesk
{
    public class HdkCategoryTicketRequestValidator: AbstractValidator<HdkCategoryTicketRequest>
    {
        public HdkCategoryTicketRequestValidator() { }
    }
}
