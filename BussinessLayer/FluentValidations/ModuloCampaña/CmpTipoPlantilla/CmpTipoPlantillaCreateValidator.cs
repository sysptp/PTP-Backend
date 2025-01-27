using BussinessLayer.DTOs.ModuloCampaña.CmpTipoPlantillas;
using BussinessLayer.FluentValidations.Generic;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCampaña.CmpTipoPlantilla
{
    public class CmpTipoPlantillaCreateValidator : AbstractValidator<CmpTipoPlantillaCreateDto>
    {
        private readonly IGenericValidation _genericValidation;

        public CmpTipoPlantillaCreateValidator(IGenericValidation genericValidation)
        {   
            _genericValidation = genericValidation;

            RuleFor(x => x.EmpresaId).NotEmpty().WithMessage("EmpresaId no puede ser vacio")
                .NotNull().WithMessage("EmpresaId no puede ser nulo")
                .GreaterThan(0).WithMessage("EmpresaId debe ser mayor a cero(0)")
                .Must(_genericValidation.ExistingBussines).WithMessage("No existe una empresa registrada con el ID proporcionado");

            RuleFor(x => x.Descripcion).NotEmpty().WithMessage("Descripcion no puede ser vacio")
                 .NotNull().WithMessage("Descripcion no puede ser nulo")
                 .MaximumLength(100).WithMessage("Descripcion no puede ser mayor a 100 caracteres");
            
            RuleFor(x => x.UsuarioAdicion).NotEmpty().WithMessage("UsuarioAdicion no puede ser vacio")
                 .NotNull().WithMessage("UsuarioAdicion no puede ser nulo")
                 .MaximumLength(50).WithMessage("UsuarioAdicion no puede ser mayor a 100 caracteres");



        }
    }
}
