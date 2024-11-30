using FluentValidation;
using BussinessLayer.DTOs.Auditoria;

namespace BussinessLayer.FluentValidations.Auditoria
{
    public class AleLoginRequestValidator : AbstractValidator<AleLoginRequest>
    {
        public AleLoginRequestValidator() { }
    }
}
