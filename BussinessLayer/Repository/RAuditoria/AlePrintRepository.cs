using BussinessLayer.Interfaces.Repository.Auditoria;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Auditoria;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.Auditoria
{
    public class AlePrintRepository : GenericRepository<AlePrint>, IAlePrintRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public AlePrintRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
