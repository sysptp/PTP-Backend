using BussinessLayer.Repository.ROtros;
using DataLayer.PDbContex;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaSessionDetailsRepository : GenericRepository<CtaSessionDetailsRequest>, ICtaSessionDetailsRepository
    {
        public CtaSessionDetailsRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
