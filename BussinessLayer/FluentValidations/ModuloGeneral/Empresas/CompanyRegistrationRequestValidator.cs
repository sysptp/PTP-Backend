using FluentValidation;
using BussinessLayer.DTOs.ModuloGeneral.Empresas;

namespace BussinessLayer.Validators.ModuloGeneral.Empresas
{
    public class CompanyRegistrationRequestValidator : AbstractValidator<CompanyRegistrationRequest>
    {
        public CompanyRegistrationRequestValidator()
        {
            RuleFor(x => x.Company)
                .NotNull()
                .WithMessage("La información de la empresa es requerida");

            RuleFor(x => x.AdminUser)
                .NotNull()
                .WithMessage("La información del usuario administrador es requerida");

            RuleFor(x => x.SelectedModuleIds)
                .NotNull()
                .NotEmpty()
                .WithMessage("Debe seleccionar al menos un módulo");

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
            });
        }
    }
}