using BussinessLayer.DTOs.ModuloCampaña.CmpContacto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.FluentValidations.ModuloCampaña.CmpContactos
{
    // Validación para la actualización de contactos
    public class CmpContactoUpdateValidator : AbstractValidator<CmpContactoUpdateDto>
    {
        public CmpContactoUpdateValidator()

        {
            RuleFor(c => c.ContactoId)
                .GreaterThan(0).WithMessage("El ContactoId debe ser mayor a 0.");

            RuleFor(c => c.ClienteId)
                .GreaterThan(0).WithMessage("El ClienteId debe ser mayor a 0.");

            RuleFor(c => c.Contacto)
                .NotEmpty().WithMessage("El campo Contacto no puede estar vacío.")
                .MaximumLength(255).WithMessage("El campo Contacto no puede exceder los 255 caracteres.");

            RuleFor(c => c.TipoContactoId)
                .GreaterThan(0).WithMessage("El TipoContactoId debe ser mayor a 0.");


            RuleFor(c => c.EmpresaId)
                .GreaterThan(0).WithMessage("El EmpresaId debe ser mayor a 0.");

            RuleFor(c => c.UsuarioModificacion)
                .NotEmpty().WithMessage("El UsuarioModificacion no puede estar vacío.")
                .MaximumLength(100).WithMessage("El UsuarioModificacion no puede exceder los 100 caracteres.");
        }
    }
}
