using FluentValidation;
using BussinessLayer.DTOs.ModuloAuditoria;

namespace BussinessLayer.FluentValidations.ModuloAuditoria
{
    public class AleLoginRequestValidator : AbstractValidator<AleLoginRequest>
    {
        public AleLoginRequestValidator() { }
    }
}
