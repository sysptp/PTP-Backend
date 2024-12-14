using BussinessLayer.DTOs.ModuloGeneral.Archivos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Archivo
{
    public class EditGnUploadFileParametroRequestValidator : AbstractValidator<EditGnUploadFileParametroDto>
    {
        public EditGnUploadFileParametroRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("El Id debe ser mayor que 0.");
            RuleFor(x => x.IdParametro).GreaterThan(0).WithMessage("El IdParametro debe ser mayor que 0.");
        }
    }
}
