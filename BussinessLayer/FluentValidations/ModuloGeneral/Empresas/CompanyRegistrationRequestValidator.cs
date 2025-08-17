using FluentValidation;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;
using BussinessLayer.Interfaces.Services.IAccount;

namespace BussinessLayer.Validators.ModuloGeneral.Empresas
{
    public class CompanyRegistrationRequestValidator : AbstractValidator<CompanyRegistrationRequest>
    {
        private readonly IAccountService _accountService;
        private List<string> _userValidationErrors = new List<string>();

        public CompanyRegistrationRequestValidator(IAccountService accountService)
        {
            _accountService = accountService;

            RuleFor(x => x.Company)
                .NotNull()
                .WithMessage("La información de la empresa es requerida");

            RuleFor(x => x.AdminUser)
                .NotNull()
                .WithMessage("La información del usuario administrador es requerida");

            RuleFor(x => x.SelectedModuleIds)
                .NotNull()
                .NotEmpty()
                .WithMessage("Debe seleccionar al menos un módulo")
                .Must(NotContainZero)
                .WithMessage("Los IDs de módulo no pueden ser 0 o negativos");

            When(x => x.AdminUser != null, () =>
            {
                RuleFor(x => x.AdminUser.FirstName)
                    .NotEmpty()
                    .WithMessage("El nombre es requerido");

                RuleFor(x => x.AdminUser.LastName)
                    .NotEmpty()
                    .WithMessage("El apellido es requerido");

                RuleFor(x => x.AdminUser.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .WithMessage("Se requiere un correo electrónico válido");

                RuleFor(x => x.AdminUser.UserName)
                    .NotEmpty()
                    .WithMessage("El nombre de usuario es requerido");

                RuleFor(x => x.AdminUser.Password)
                    .NotEmpty()
                    .MinimumLength(6)
                    .WithMessage("La contraseña debe tener al menos 6 caracteres");

                RuleFor(x => x.AdminUser.ConfirmPassword)
                    .Equal(x => x.AdminUser.Password)
                    .WithMessage("Las contraseñas no coinciden");

                RuleFor(x => x.AdminUser.Phone)
                    .NotEmpty()
                    .WithMessage("El número de teléfono es requerido");

                RuleFor(x => x)
                    .MustAsync(async (request, cancellation) =>
                    {
                        _userValidationErrors = await _accountService.ValidateUserRegistrationAsync(
                            request.AdminUser.UserName,
                            request.AdminUser.Email);
                        return !_userValidationErrors.Any();
                    })
                    .WithMessage(request => string.Join(", ", _userValidationErrors));
            });
        }

        private bool NotContainZero(List<int> moduleIds)
        {
            return moduleIds != null && !moduleIds.Any(id => id <= 0);
        }
    }
}