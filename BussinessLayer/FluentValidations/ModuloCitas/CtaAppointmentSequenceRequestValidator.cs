using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Empresa;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCitas
{
    public class CtaAppointmentSequenceRequestValidator : AbstractValidator<CtaAppointmentSequenceRequest>
    {
        private readonly IGnEmpresaRepository _companyRepository;
        private readonly ICtaAppointmentAreaRepository _areaRepository;

        public CtaAppointmentSequenceRequestValidator(IGnEmpresaRepository companyRepository, ICtaAppointmentAreaRepository areaRepository)
        {
            _companyRepository = companyRepository;
            _areaRepository = areaRepository;

            RuleFor(x => x.MaxValue)
             .GreaterThan(x => x.MinValue)
             .When(x => x.MaxValue > 0)
             .WithMessage("El valor máximo (MaxValue) debe ser mayor que el valor mínimo (MinValue) si no es cero.");

            RuleFor(x => x.SequenceIdentifier)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.SequenceIdentifier))
                .WithMessage("El identificador (SequenceIdentifier) no debe exceder los 100 caracteres.");

            RuleFor(x => x.Prefix)
                .MaximumLength(50)
                .When(x => !string.IsNullOrEmpty(x.Prefix))
                .WithMessage("El prefijo (Prefix) no debe exceder los 50 caracteres.");

            RuleFor(x => x.Suffix)
                .MaximumLength(50)
                .When(x => !string.IsNullOrEmpty(x.Suffix))
                .WithMessage("El sufijo (Suffix) no debe exceder los 50 caracteres.");

            RuleFor(x => x.IncrementBy)
           .GreaterThan(0)
           .WithMessage("La cantidad a incrementar (IncrementBy) debe ser mayor o igual a 1");

            RuleFor(x => x.CompanyId)
                .MustAsync(async (companyId, cancellation) =>
                {
                    return await _companyRepository.GetById(companyId) != null;
                })
                .WithMessage("El identificador de la empresa (CompanyId) no existe en la base de datos.");

            RuleFor(x => x.AreaId)
                .MustAsync(async (areaId, cancellation) =>
                {
                    return areaId == null || areaId == 0 || await _areaRepository.GetById(areaId) != null;
                })
                .WithMessage("El identificador del área (AreaId) no existe en la base de datos.");
            
        }
    }
}
