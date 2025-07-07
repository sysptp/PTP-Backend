using BussinessLayer.DTOs.ModuloCitas.BookingPortal;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCitas
{
    public class ClientAuthenticationRequestValidator : AbstractValidator<ClientAuthenticationRequest>
    {
        public ClientAuthenticationRequestValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Número de teléfono es requerido")
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Formato de teléfono inválido");

            RuleFor(x => x.PortalSlug)
                .NotEmpty()
                .WithMessage("Slug del portal es requerido");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0)
                .WithMessage("ID de empresa es requerido");
        }
    }
}
