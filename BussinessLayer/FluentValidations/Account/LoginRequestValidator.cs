<<<<<<<< HEAD:BussinessLayer/FluentValidations/ModuloGeneral/Configuracion/Account/LoginRequestValidator.cs
﻿using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Autenticacion;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloGeneral.Configuracion.Account
========
﻿using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Autenticacion;
using FluentValidation;

namespace BussinessLayer.FluentValidations.Account
>>>>>>>> REFACTOR:BussinessLayer/FluentValidations/Account/LoginRequestValidator.cs
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.User)
                .NotEmpty().WithMessage("El campo usuario no puede estar vacío.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("El campo contraseña no puede estar vacío.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.")
                .Matches("[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula.")
                .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un número.")
                .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial.");
        }
    }


}
