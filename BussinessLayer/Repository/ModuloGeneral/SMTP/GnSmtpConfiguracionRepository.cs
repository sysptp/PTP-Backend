using BussinessLayer.Interfaces.Repository.ModuloGeneral.SMTP;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.SMTP;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloGeneral.SMTP
{
    public class GnSmtpConfiguracionRepository : GenericRepository<GnSmtpConfiguracion>, IGnSmtpConfiguracionRepository
    {
        private readonly PDbContext _dbContext;
        public GnSmtpConfiguracionRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
        }

        public async Task<GnSmtpConfiguracion> GetSMTPByCompanyId(long companyId)
        {
            var smtpServer = _context.GnSmtpConfiguracion.
            Where(x => x.IdEmpresa == companyId && x.Borrado != true).FirstOrDefault();

            return smtpServer;
        }
    }
}
