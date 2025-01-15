using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCampaña.CmpSmtpConfiguraciones
{
    public class CmpServidoresSmtpCreateValidator : AbstractValidator<CmpServidoresSmtpCreateDto>
    {
        public CmpServidoresSmtpCreateValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.Host).NotEmpty().WithMessage("El host es obligatorio.");
            RuleFor(x => x.Puerto).GreaterThan(0).WithMessage("El puerto debe ser mayor a 0.");
        }
    }
}
