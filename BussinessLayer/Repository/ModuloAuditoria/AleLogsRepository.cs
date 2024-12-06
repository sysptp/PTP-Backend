using BussinessLayer.Interfaces.Repository.ModuloAuditoria;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloAuditoria;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloAuditoria
{
    public class AleLogsRepository : GenericRepository<AleLogs>, IAleLogsRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public AleLogsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
