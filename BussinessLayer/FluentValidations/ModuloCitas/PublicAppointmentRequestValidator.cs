using BussinessLayer.DTOs.ModuloCitas.BookingPortal;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCitas
{
    public class PublicAppointmentRequestValidator : AbstractValidator<PublicAppointmentRequest>
    {
        public PublicAppointmentRequestValidator()
        {
            RuleFor(x => x.PortalSlug)
                .NotEmpty()
                .WithMessage("Slug del portal es requerido");

            RuleFor(x => x.AppointmentDate)
                .GreaterThan(DateTime.Today)
                .WithMessage("La fecha de cita debe ser futura");

            RuleFor(x => x.AppointmentTime)
                .NotEmpty()
                .WithMessage("Hora de cita es requerida");

            // Validaciones condicionales para nuevos clientes
            When(x => string.IsNullOrEmpty(x.AuthToken), () => {
                RuleFor(x => x.ClientName)
                    .NotEmpty()
                    .WithMessage("Nombre del cliente es requerido para nuevos registros");

                RuleFor(x => x.ClientPhone)
                    .NotEmpty()
                    .WithMessage("Teléfono del cliente es requerido para nuevos registros")
                    .Matches(@"^\+?[1-9]\d{1,14}$")
                    .WithMessage("Formato de teléfono inválido");

                RuleFor(x => x.ClientEmail)
                    .EmailAddress()
                    .When(x => !string.IsNullOrEmpty(x.ClientEmail))
                    .WithMessage("Formato de email inválido");
            });
        }
    }
}
