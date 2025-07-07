using BussinessLayer.DTOs.ModuloCitas.BookingPortal;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCitas
{
    public class BookingPortalConfigRequestValidator : AbstractValidator<BookingPortalConfigRequest>
    {
        public BookingPortalConfigRequestValidator()
        {
            RuleFor(x => x.CompanyId)
                .GreaterThan(0)
                .WithMessage("ID de empresa es requerido");

            RuleFor(x => x.PortalName)
                .NotEmpty()
                .WithMessage("Nombre del portal es requerido")
                .MaximumLength(100)
                .WithMessage("Nombre del portal no debe exceder 100 caracteres");

            RuleFor(x => x.AssignedUserId)
                .GreaterThan(0)
                .WithMessage("Usuario asignado es requerido");

            RuleFor(x => x.CustomSlug)
                .Matches("^[a-z0-9-]+$")
                .When(x => !string.IsNullOrEmpty(x.CustomSlug))
                .WithMessage("El slug solo puede contener letras minúsculas, números y guiones");

            RuleFor(x => x.DefaultAppointmentDuration)
                .Must(duration => duration == null || duration.Value.TotalMinutes >= 15)
                .WithMessage("La duración mínima de cita debe ser 15 minutos");

            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime)
                .When(x => x.StartTime.HasValue && x.EndTime.HasValue)
                .WithMessage("Hora de inicio debe ser menor a hora de fin");

            RuleFor(x => x.MaxAdvanceDays)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(365)
                .When(x => x.MaxAdvanceDays.HasValue)
                .WithMessage("Días de anticipación debe estar entre 1 y 365");
        }
    }
}