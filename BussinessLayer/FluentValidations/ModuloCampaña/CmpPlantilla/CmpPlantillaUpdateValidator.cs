using BussinessLayer.DTOs.ModuloCampaña.CmpPlantillas;
using BussinessLayer.FluentValidations.Generic;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCampaña.CmpPlantilla
{
    public class CmpPlantillaUpdateValidator : AbstractValidator<CmpPlantillaUpdateDto>
    {
        private readonly IGenericValidation _generic;
        public CmpPlantillaUpdateValidator(IGenericValidation generic)
        {
            _generic = generic;

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id no puede ser vacio")
                .Must(_generic.ExistePlantilla).WithMessage("El ID proporcionado no pertenece a ninguna plantilla")
                .GreaterThan(0).WithMessage("Id debe ser mayor a cero (0).");

            RuleFor(x => x.Nombre).MaximumLength(100).WithMessage("Nombre no debe ser mayor a 100 caracteres")
                .NotEmpty().WithMessage("Nombre no puede ser vacio")
                .NotNull().WithMessage("Nombre no puede ser nulo");

            RuleFor(x => x.Contenido).NotEmpty().WithMessage("Contenido no puede ser vacio")
                .NotNull().WithMessage("Contenido no puede ser nulo");

            RuleFor(x => x.TipoPlantillaId).NotNull().WithMessage("TipoPlantillaId no puede ser nulo")
                .GreaterThan(0).WithMessage("TipoPlantillaId debe ser mayor a 0")
                .Must(_generic.ValidarTipoPlantilla);

            RuleFor(x => x.EmpresaId).GreaterThan(0).WithMessage("Empresa Id debe ser mayor a 0")
               .Must(_generic.ExistingBussines).WithMessage("La empresa con el ID proporcionado no existe");

            RuleFor(x => x.UsuarioModificacion).NotNull().WithMessage("UsuarioModificacion no puede ser nulo o vacio")
            .MinimumLength(5).WithMessage("UsuarioModificacion debe ser minimo de 5 caracteres");
        }
    }
}
