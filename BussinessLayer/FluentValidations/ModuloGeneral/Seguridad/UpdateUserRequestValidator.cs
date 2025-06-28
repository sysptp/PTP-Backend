using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Usuario;
using FluentValidation;

namespace BussinessLayer.FluentValidations.Usuario
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly IGnPerfilRepository _gnPerfilRepository;
        private readonly IGnSucursalRepository _sucursalRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public UpdateUserRequestValidator(IGnEmpresaRepository empresaRepository,
            IGnPerfilRepository gnPerfilRepository,
            IGnSucursalRepository sucursalRepository,
            IUsuarioRepository usuarioRepository)
        {
            _empresaRepository = empresaRepository;
            _gnPerfilRepository = gnPerfilRepository;
            _sucursalRepository = sucursalRepository;
            _usuarioRepository = usuarioRepository;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es requerido.")
                .MaximumLength(100).WithMessage("El apellido no puede exceder 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es requerido.")
                .EmailAddress().WithMessage("La dirección de correo electrónico no es válida.")
                .MaximumLength(256).WithMessage("El correo electrónico no puede exceder 256 caracteres.")
                .Must( (model, email, cancellation) => !EmailNotInUseByOtherUser(email,model.Email))
                .WithMessage("El correo electrónico ya está en uso por otro usuario.");

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("El número de teléfono no puede exceder 20 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("El ID del role debe ser mayor a 0.")
                .MustAsync(async (roleId, cancellation) => await RoleExists(roleId))
                .WithMessage("El ID del role no es válido.");

            RuleFor(x => x.SucursalId)
                .GreaterThan(0).WithMessage("El ID de la sucursal debe ser mayor a 0.")
                .MustAsync(async (sucursalId, cancellation) => await SucursalExists(sucursalId))
                .WithMessage("El ID de la sucursal no es válido.");

            RuleFor(x => x.ScheduleId)
                .GreaterThanOrEqualTo(0).WithMessage("El ID del horario no puede ser negativo.");

            RuleFor(x => x.UserIP)
                .MaximumLength(45).WithMessage("La IP del usuario no puede exceder 45 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.UserIP));

            RuleFor(x => x.LanguageCode)
                .MaximumLength(10).WithMessage("El código de idioma no puede exceder 10 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.LanguageCode));
        }

        private async Task<bool> RoleExists(int id)
        {
            var role = await _gnPerfilRepository.GetById(id);
            return role != null;
        }

        private async Task<bool> SucursalExists(long id)
        {
            var sucursal = await _sucursalRepository.GetById(id);
            return sucursal != null;
        }

        private bool EmailNotInUseByOtherUser(string email,string modelEmail)
        {
            return _usuarioRepository.EmailExists(email) && email.ToLower() != modelEmail.ToLower();
        }
    }
}