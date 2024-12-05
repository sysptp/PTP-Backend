using BussinessLayer.Interfaces.Repository.HelpDesk;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloHelpDesk;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloHelpDesk
{
    public class HdkTicketsRepository : GenericRepository<HdkTickets>, IHdkTicketsRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public HdkTicketsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
