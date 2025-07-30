using BussinessLayer.DTOs.ModuloCitas.CtaUserNotificationReads;
using FluentValidation;

namespace BussinessLayer.FluentValidations.ModuloCitas
{
    public class CtaUserNotificationReadsRequestValidator : AbstractValidator<CtaUserNotificationReadsRequest>
    {
        public CtaUserNotificationReadsRequestValidator()
        {
            
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId es obligatorio.");
            
            RuleFor(x => x.NotificationId)
                .NotEmpty().NotNull().WithMessage("NotificationId es obligatorio.");
            
        }
    }
}
