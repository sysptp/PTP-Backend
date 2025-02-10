using BussinessLayer.DTOs.ModuloGeneral.Email;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Email
{
    public interface IGnEmailService
    {
        Task SendAsync(GnEmailMessageDto request, long companyId);
    }
}
