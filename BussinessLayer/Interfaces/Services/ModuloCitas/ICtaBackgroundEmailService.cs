using BussinessLayer.DTOs.ModuloCitas.CtaEmailBackgroundJobData;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaBackgroundEmailService
    {
        void QueueAppointmentEmails(CtaEmailBackgroundJobData emailData);
    }
}
