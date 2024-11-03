using BussinessLayer.DTOs.Seguridad;
using FluentValidation;

namespace BussinessLayer.FluentValidations.Seguridad
{
  
    public class GnPerfilRequestValidator : AbstractValidator<GnPerfilRequest>
    {
        public GnPerfilRequestValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("El nombre del perfil es obligatorio.");
        }
    }

}
