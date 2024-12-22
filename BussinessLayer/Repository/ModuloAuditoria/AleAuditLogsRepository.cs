using BussinessLayer.Interfaces.Repository.ModuloAuditoria;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloAuditoria;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloAuditoria
{
    public class AleAuditLogsRepository : GenericRepository<AleAuditLogs>, IAleAuditLogsRepository
    {
        public AleAuditLogsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
