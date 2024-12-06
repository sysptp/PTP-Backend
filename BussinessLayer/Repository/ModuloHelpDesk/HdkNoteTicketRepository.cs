using BussinessLayer.Interfaces.Repository.ModuloHelpDesk;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloHelpDesk;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloHelpDesk
{
    public class HdkNoteTicketRepository : GenericRepository<HdkNoteTicket>, IHdkNoteTicketRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public HdkNoteTicketRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
