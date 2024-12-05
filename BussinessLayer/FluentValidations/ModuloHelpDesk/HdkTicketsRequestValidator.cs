using BussinessLayer.DTOs.HelpDesk;
using FluentValidation;


namespace BussinessLayer.FluentValidations.ModuloHelpDesk
{
    public class HdkTicketsRequestValidator : AbstractValidator<HdkTicketsRequest>
    {
        public HdkTicketsRequestValidator() { }
    }
}
