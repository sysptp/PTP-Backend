using BussinessLayer.Interfaces.Repository.Auditoria;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloAuditoria;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloAuditoria
{
    public class AleLoginRepository : GenericRepository<AleLogin>, IAleLoginRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public AleLoginRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
