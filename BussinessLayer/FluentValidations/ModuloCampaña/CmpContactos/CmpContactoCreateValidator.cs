using BussinessLayer.DTOs.ModuloCampaña.CmpContacto;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCampaña.CmpContactos
{
    // Validación para la creación de contactos
    public class CmpContactoCreateValidator : AbstractValidator<CmpContactoCreateDto>
    {
        public CmpContactoCreateValidator()
        {
            RuleFor(c => c.ClienteId)
                .GreaterThan(0).WithMessage("El ClienteId debe ser mayor a 0.");

            RuleFor(c => c.Contacto)
                .NotEmpty().WithMessage("El campo Contacto no puede estar vacío.")
                .MaximumLength(255).WithMessage("El campo Contacto no puede exceder los 255 caracteres.");

            RuleFor(c => c.TipoContactoId)
                .GreaterThan(0).WithMessage("El TipoContactoId debe ser mayor a 0.");

            RuleFor(c => c.Estado)
                .NotNull().WithMessage("El Estado es obligatorio.");

            RuleFor(c => c.EmpresaId)
                .GreaterThan(0).WithMessage("El EmpresaId debe ser mayor a 0.");

            RuleFor(c => c.UsuarioCreacion)
                .NotEmpty().WithMessage("El UsuarioCreacion no puede estar vacío.")
                .MaximumLength(100).WithMessage("El UsuarioCreacion no puede exceder los 100 caracteres.");
        }
    }

}
