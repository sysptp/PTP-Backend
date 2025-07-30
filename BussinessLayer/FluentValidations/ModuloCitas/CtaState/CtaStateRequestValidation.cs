using BussinessLayer.DTOs.ModuloCitas.CtaState;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using FluentValidation;

namespace BussinessLayer.Validations.ModuloCitas.CtaState
{
    public class CtaStateRequestValidation : AbstractValidator<CtaStateRequest>
    {
        private readonly ICtaStateRepository _ctaStateRepository;

        public CtaStateRequestValidation(ICtaStateRepository ctaStateRepository)
        {
            _ctaStateRepository = ctaStateRepository;

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción es requerida.")
                .MaximumLength(200).WithMessage("La descripción no puede tener más de 200 caracteres.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("Debe especificar una empresa válida.");

            RuleFor(x => x.AreaId)
                .GreaterThan(0).WithMessage("Debe especificar un área válida.");

            When(x => x.IsDefault, () =>
            {
                RuleFor(x => x)
                    .MustAsync(BeOnlyOneDefaultPerCompanyAndArea).WithMessage("Ya existe un estado por defecto para esta empresa y área.");
            });

            When(x => x.IsClosure, () =>
            {
                RuleFor(x => x)
                    .MustAsync(BeOnlyOneClosurePerCompanyAndArea).WithMessage("Ya existe un estado de cierre para esta empresa y área.");
            });
        }

        private async Task<bool> BeOnlyOneDefaultPerCompanyAndArea(CtaStateRequest request, CancellationToken cancellationToken)
        {
            var existing = await _ctaStateRepository.GetDefaultStateByCompanyAndAreaAsync(request.CompanyId, request.AreaId);
            return existing == null || existing.IdStateAppointment == request.IdStateAppointment;
        }

        private async Task<bool> BeOnlyOneClosurePerCompanyAndArea(CtaStateRequest request, CancellationToken cancellationToken)
        {
            var existing = await _ctaStateRepository.GetClosureStateByCompanyAndAreaAsync(request.CompanyId, request.AreaId);
            return existing == null || existing.IdStateAppointment == request.IdStateAppointment;
        }
    }
}
