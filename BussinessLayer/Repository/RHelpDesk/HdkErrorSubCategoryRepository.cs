using BussinessLayer.Interfaces.Repository.HelpDesk;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloHelpDesk;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.HelpDesk
{
    public class HdkErrorSubCategoryRepository : GenericRepository<HdkErrorSubCategory>, IHdkErrorSubCategoryRepository
    {
        private readonly PDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public HdkErrorSubCategoryRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
    }
}
