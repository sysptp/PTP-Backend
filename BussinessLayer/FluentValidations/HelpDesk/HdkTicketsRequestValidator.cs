using BussinessLayer.DTOs.HelpDesk;
using FluentValidation;


namespace BussinessLayer.FluentValidations.HelpDesk
{
    public class HdkTicketsRequestValidator : AbstractValidator<HdkTicketsRequest>
    {
        public HdkTicketsRequestValidator() { }
    }
}
