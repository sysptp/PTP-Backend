using BussinessLayer.Interfaces.Repository.ModuloGeneral.SMTP;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.SMTP;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ModuloGeneral.SMTP
{

    // 1. Modifica GnSmtpConfiguracionRepository para usar DbContextFactory
    public class GnSmtpConfiguracionRepository : GenericRepository<GnSmtpConfiguracion>, IGnSmtpConfiguracionRepository
    {
        private readonly IDbContextFactory<PDbContext> _contextFactory;

        public GnSmtpConfiguracionRepository(PDbContext dbContext, ITokenService tokenService, IDbContextFactory<PDbContext> contextFactory)
            : base(dbContext, tokenService)
        {
            _contextFactory = contextFactory;
        }

        public GnSmtpConfiguracion GetSMTPByCompanyId(long companyId)
        {
            var smtpServer = _context.GnSmtpConfiguracion
                .Where(x => x.IdEmpresa == companyId && x.Borrado != true)
                .FirstOrDefault();
            return smtpServer;
        }

        // Método adicional para usar con tareas en segundo plano
        public async Task<GnSmtpConfiguracion> GetSMTPByCompanyIdAsync(long companyId)
        {
            // Aquí usamos una nueva instancia del contexto
            using var context = await _contextFactory.CreateDbContextAsync();
            var smtpServer = await context.GnSmtpConfiguracion
                .Where(x => x.IdEmpresa == companyId && x.Borrado != true)
                .FirstOrDefaultAsync();
            return smtpServer;
        }

    }

}
