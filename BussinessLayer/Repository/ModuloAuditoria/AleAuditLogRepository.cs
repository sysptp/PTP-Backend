using BussinessLayer.Interfaces.Repository.ModuloAuditoria;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloAuditoria;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloAuditoria
{
    public class AleAuditLogRepository : GenericRepository<AleAuditLog>, IAleAuditLogRepository
    {
        public AleAuditLogRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
