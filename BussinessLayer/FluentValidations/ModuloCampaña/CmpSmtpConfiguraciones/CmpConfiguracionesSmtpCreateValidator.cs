using BussinessLayer.DTOs.ModuloCampaña.CmpConfiguraciones;
using BussinessLayer.FluentValidations.Generic;
using DataLayer.PDbContex;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCampaña.CmpSmtpConfiguraciones
{
    public class CmpConfiguracionesSmtpCreateValidator : AbstractValidator<CmpConfiguracionCreateDto>
    {
        private readonly IGenericValidation _genericValidation;
        public CmpConfiguracionesSmtpCreateValidator(IGenericValidation genericValidation)
        {
            _genericValidation = genericValidation;

            RuleFor(x => x.EmpresaId).GreaterThan(0).WithMessage("Empresa Id debe ser mayor a 0")
                .Must(_genericValidation.ExistingBussines).WithMessage("La empresa con el ID proporcionado no existe");

            RuleFor(x => x.ServidorId).GreaterThan(0).WithMessage("Servidor Id debe ser mayor a 0")
                .Must(_genericValidation.ExistingServer).WithMessage("El servidor con el ID proporcionado no existe");

            RuleFor(x => x.Contraseña).NotNull().WithMessage("Contraseña no puede ser nula o vacia")
                .MinimumLength(10).WithMessage("Contraseña debe ser minimo de 10 caracteres");

            RuleFor(x => x.Usuario).NotNull().WithMessage("Usuario no puede ser nulo o vacio")
                .MinimumLength(10).WithMessage("Usuario debe ser minimo de 10 caracteres"); 
            
            //RuleFor(x => x.Em).NotNull().WithMessage("Usuario no puede ser nulo o vacio")
            //    .MinimumLength(10).WithMessage("Usuario debe ser minimo de 10 caracteres");

            RuleFor(x => x.UsuarioAdicion).NotNull().WithMessage("Usuario Adicion no puede ser nulo o vacio")
            .MinimumLength(5).WithMessage("Usuario Adicion debe ser minimo de 5 caracteres");
        }
    }
}
