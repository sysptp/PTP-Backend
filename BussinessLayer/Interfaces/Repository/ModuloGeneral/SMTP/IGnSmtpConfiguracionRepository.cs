using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloGeneral.SMTP;

namespace BussinessLayer.Interfaces.Repository.ModuloGeneral.SMTP
{
    public interface IGnSmtpConfiguracionRepository : IGenericRepository<GnSmtpConfiguracion>
    {
        GnSmtpConfiguracion GetSMTPByCompanyId(long companyId);
        Task<GnSmtpConfiguracion> GetSMTPByCompanyIdAsync(long companyId);
    }
}
