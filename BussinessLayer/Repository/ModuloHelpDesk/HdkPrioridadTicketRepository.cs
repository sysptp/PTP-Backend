using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloHelpDesk;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloHelpDesk
{
    public class HdkPrioridadTicketRepository : GenericRepository<HdkPrioridadTicket>, IHdkPrioridadTicketRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public HdkPrioridadTicketRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
