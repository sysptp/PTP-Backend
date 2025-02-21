using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using FluentValidation;

namespace BussinessLayer.Validations.ModuloCitas.CtaSessions
{
    public class CtaSessionsRequestValidation : AbstractValidator<CtaSessionsRequest>
    {
        public CtaSessionsRequestValidation() 
        {

            RuleFor(x => x.AppointmentInformation.AppointmentParticipants)
                .Must(participants => participants.Any())
            .WithMessage("Debe haber al menos un participante");

        }
    }
}

