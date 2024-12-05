using BussinessLayer.Repository.ROtros;
using DataLayer.PDbContex;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaUnwantedRepository : GenericRepository<CtaUnwanted>, ICtaUnwantedRepository
    {
        public CtaUnwantedRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
