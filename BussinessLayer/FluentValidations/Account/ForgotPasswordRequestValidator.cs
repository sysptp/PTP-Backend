using BussinessLayer.DTOs.Account;
using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Account;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using FluentValidation;

namespace BussinessLayer.FluentValidations.Account
{
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ForgotPasswordRequestValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es requerido.")
                .EmailAddress().WithMessage("La dirección de correo electrónico no es válida.")
                .MustAsync(async (email, cancellation) => await EmailExistsAsync(email))
                .WithMessage("No existe una cuenta asociada a este correo electrónico.");
        }

        private async Task<bool> EmailExistsAsync(string email)
        {
            return _usuarioRepository.EmailExists(email);
        }
    }

}