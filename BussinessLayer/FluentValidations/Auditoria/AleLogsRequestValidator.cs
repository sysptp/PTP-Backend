using FluentValidation;
using BussinessLayer.DTOs.Auditoria;

namespace BussinessLayer.FluentValidations.Auditoria
{
    public class AleLogsRequestValidator: AbstractValidator<AleLogsRequest>
    {
        public AleLogsRequestValidator() { }
    }
}
