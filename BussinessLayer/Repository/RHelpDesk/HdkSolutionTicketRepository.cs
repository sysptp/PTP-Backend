using BussinessLayer.Interfaces.Repository.HelpDesk;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.HelpDesk;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.HelpDesk
{
    public class HdkSolutionTicketRepository : GenericRepository<HdkSolutionTicket>, IHdkSolutionTicketRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public HdkSolutionTicketRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
