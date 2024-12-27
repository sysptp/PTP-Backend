using BussinessLayer.DTOs.ModuloGeneral.Archivos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Archivo
{
    public class EditGnTecnoAlmacenExternoDtoValidator : AbstractValidator<EditGnTecnoAlmacenExternoDto>
    {
        public EditGnTecnoAlmacenExternoDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El Id es obligatorio.");
            RuleFor(x => x.Descripcion).MaximumLength(150).WithMessage("La Descripcion no puede superar los 150 caracteres.");
            RuleFor(x => x.UsuaridioExteno).MaximumLength(50).WithMessage("El Usuario Externo no puede superar los 50 caracteres.");
            RuleFor(x => x.PassWordExt).MaximumLength(200).WithMessage("El Password Externo no puede superar los 200 caracteres.");
        }
    }
}
