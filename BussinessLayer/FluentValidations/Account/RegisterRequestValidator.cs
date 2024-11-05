using BussinessLayer.Dtos.Account;
using FluentValidation;
using BussinessLayer.Interfaces.Repository.Empresa;
using BussinessLayer.Repository.RSeguridad;

namespace BussinessLayer.FluentValidations.Account
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        private readonly IGnEmpresaRepository _empresaRepository;
        private readonly IGnPerfilRepository _gnPerfilRepository;
        private readonly IGnSucursalRepository _sucursalRepository;

        public RegisterRequestValidator(IGnEmpresaRepository empresaRepository, 
            IGnPerfilRepository gnPerfilRepository,
            IGnSucursalRepository sucursalRepository)
        {
            _empresaRepository = empresaRepository;
            _gnPerfilRepository = gnPerfilRepository;
            _sucursalRepository = sucursalRepository;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("El nombre es requerido.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("El apellido es requerido.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es requerido.")
                .EmailAddress().WithMessage("La dirección de correo electrónico no es válida.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("El nombre de usuario es requerido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.")
                .Matches("[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula.")
                .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un dígito.")
                .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("La confirmación de la contraseña debe coincidir con la contraseña.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("El número de teléfono es requerido.");

            RuleFor(x => x.CompanyId)
                .MustAsync(async (companyId, cancellation) => await CompanyExists(companyId))
                .WithMessage("El ID de la compañía no es válido.");

            RuleFor(x => x.RoleId)
               .MustAsync(async (roleId, cancellation) => await RoleExists(roleId))
               .WithMessage("El ID del role no es válido.");

            RuleFor(x => x.SucursalId)
               .MustAsync(async (sucursalId, cancellation) => await SucursalExists(sucursalId))
               .WithMessage("El ID de la sucursal no es válido.");
        }

        private async Task<bool> CompanyExists(long companyId)
        {
            var company = await _empresaRepository.GetById((long)companyId);
            return company != null;
        }

        private async Task<bool> RoleExists(int id)
        {
            var role = await _gnPerfilRepository.GetById(id);
            return role != null;
        }

        private async Task<bool> SucursalExists(long id)
        {
            var sucursal = await _sucursalRepository.GetBySucursalCode(id);
            return sucursal != null;
        }
    }
}
