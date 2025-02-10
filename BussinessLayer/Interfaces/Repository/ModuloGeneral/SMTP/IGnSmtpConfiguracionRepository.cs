using BussinessLayer.Interfaces.Repositories;
using DataLayer.Models.ModuloGeneral.SMTP;

namespace BussinessLayer.Interfaces.Repository.ModuloGeneral.SMTP
{
    public interface IGnSmtpConfiguracionRepository : IGenericRepository<GnSmtpConfiguracion>
    {
        Task<GnSmtpConfiguracion> GetSMTPByCompanyId(long companyId);
    }
}
