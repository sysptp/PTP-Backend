using BussinessLayer.Repository.ROtros;
using DataLayer.PDbContex;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaEmailConfiguracionRepository : GenericRepository<CtaEmailConfiguracion>, ICtaEmailConfiguracionRepository
    {
        public CtaEmailConfiguracionRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
