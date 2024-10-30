using BussinessLayer.DTOs.Seguridad;
using FluentValidation;

namespace BussinessLayer.FluentValidations.Seguridad
{
  
    public class GnPerfilDtoValidator : AbstractValidator<GnPerfilDto>
    {
        public GnPerfilDtoValidator()
        {
            RuleFor(p => p.Perfil).NotEmpty().WithMessage("El nombre del perfil es obligatorio.");
        }
    }

}
