using BussinessLayer.DTOs.ModuloCampaña.CmpEmail;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloCampaña.CmpSmtpConfiguraciones
{
    public class CmpServidoresSmtpUpdateValidator : AbstractValidator<CmpServidoresSmtpUpdateDto>
    {
        public CmpServidoresSmtpUpdateValidator()
        {
            RuleFor(x => x.ServidorId).GreaterThan(0).WithMessage("El ID del servidor debe ser mayor a 0.");
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.Host).NotEmpty().WithMessage("El host es obligatorio.");
            RuleFor(x => x.Puerto).GreaterThan(0).WithMessage("El puerto debe ser mayor a 0.");
        }
    }
}
