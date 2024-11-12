using BussinessLayer.Interfaces.Repository.HelpDesk;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.HelpDesk;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.HelpDesk
{
    public class HdkFileEvidenceTicketRepository : GenericRepository<HdkFileEvidenceTicket>, IHdkFileEvidenceTicketRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public HdkFileEvidenceTicketRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
