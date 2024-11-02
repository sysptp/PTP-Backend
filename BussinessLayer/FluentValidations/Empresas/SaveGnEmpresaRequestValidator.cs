
namespace BussinessLayer.FluentValidations.Empresas
{
    using FluentValidation;
    using global::BussinessLayer.DTOs.Empresas;

    namespace BussinessLayer.Validations
    {
        public class SaveGnEmpresaRequestValidator : AbstractValidator<SaveGnEmpresaDto>
        {
            public SaveGnEmpresaRequestValidator()
            {
                RuleFor(x => x.NOMBRE_EMP)
                    .NotEmpty().WithMessage("El nombre de la empresa es requerido.")
                    .Length(1, 100).WithMessage("El nombre de la empresa no debe exceder los 100 caracteres.");

                RuleFor(x => x.RNC_EMP)
                    .NotEmpty().WithMessage("El RNC de la empresa es requerido.")
                    .Length(1, 15).WithMessage("El RNC no debe exceder los 15 caracteres.");

                RuleFor(x => x.DIRECCION)
                    .NotEmpty().WithMessage("La dirección es requerida.")
                    .Length(1, 300).WithMessage("La dirección no debe exceder los 300 caracteres.");

                RuleFor(x => x.TELEFONO1)
                    .NotEmpty().WithMessage("El teléfono principal es requerido.")
                    .Length(1, 15).WithMessage("El teléfono principal no debe exceder los 15 caracteres.");

                RuleFor(x => x.TELEFONO2)
                    .Length(0, 15).WithMessage("El teléfono secundario no debe exceder los 15 caracteres.")
                    .When(x => !string.IsNullOrEmpty(x.TELEFONO2));

                RuleFor(x => x.EXT_TEL1)
                    .Length(0, 10).WithMessage("La extensión del teléfono 1 no debe exceder los 10 caracteres.")
                    .When(x => !string.IsNullOrEmpty(x.EXT_TEL1));

                RuleFor(x => x.EXT_TEL2)
                    .Length(0, 10).WithMessage("La extensión del teléfono 2 no debe exceder los 10 caracteres.")
                    .When(x => !string.IsNullOrEmpty(x.EXT_TEL2));

                RuleFor(x => x.CANT_SUCURSALES)
                    .GreaterThanOrEqualTo(0).WithMessage("La cantidad de sucursales debe ser mayor a 0.");

                RuleFor(x => x.CANT_USUARIO)
                    .GreaterThanOrEqualTo(0).WithMessage("La cantidad de usuarios debe ser mayor a 0.");

            }
        }
    }

}
