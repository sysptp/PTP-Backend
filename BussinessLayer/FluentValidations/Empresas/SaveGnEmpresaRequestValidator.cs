using FluentValidation;
using BussinessLayer.DTOs.Empresas;

namespace BussinessLayer.FluentValidations.Empresas
{

    namespace BussinessLayer.FluentValidations.Empresas
    {

        public class SaveGnEmpresaRequestValidator : AbstractValidator<GnEmpresaRequest>
        {
            public SaveGnEmpresaRequestValidator()
            {
                RuleFor(x => x.CompanyName)
                    .NotEmpty().WithMessage("El nombre de la empresa es requerido.")
                    .Length(1, 60).WithMessage("El nombre de la empresa no debe exceder los 60 caracteres.");

                RuleFor(x => x.RNC)
                    .NotEmpty().WithMessage("El RNC de la empresa es requerido.")
                    .Length(1, 11).WithMessage("El RNC no debe exceder los 11 caracteres.");

                RuleFor(x => x.Address)
                    .NotEmpty().WithMessage("La dirección es requerida.")
                    .Length(1, 200).WithMessage("La dirección no debe exceder los 200 caracteres.");

                RuleFor(x => x.PrimaryPhone)
                    .NotEmpty().WithMessage("El teléfono principal es requerido.")
                    .Length(1, 15).WithMessage("El teléfono principal no debe exceder los 15 caracteres.");

                RuleFor(x => x.SecondaryPhone)
                    .Length(0, 15).WithMessage("El teléfono secundario no debe exceder los 15 caracteres.")
                    .When(x => !string.IsNullOrEmpty(x.SecondaryPhone));

                RuleFor(x => x.PrimaryExtension)
                    .Length(0, 10).WithMessage("La extensión del teléfono principal no debe exceder los 10 caracteres.")
                    .When(x => !string.IsNullOrEmpty(x.PrimaryExtension));

                RuleFor(x => x.SecondaryExtension)
                    .Length(0, 10).WithMessage("La extensión del teléfono secundario no debe exceder los 10 caracteres.")
                    .When(x => !string.IsNullOrEmpty(x.SecondaryExtension));

                RuleFor(x => x.SucursalCount)
                    .GreaterThanOrEqualTo(1).WithMessage("La cantidad de sucursales debe ser mayor o igual a 1.");

                RuleFor(x => x.UserCount)
                    .GreaterThanOrEqualTo(1).WithMessage("La cantidad de usuarios debe ser mayor o igual a 1.");
            }
        }
    }

}
