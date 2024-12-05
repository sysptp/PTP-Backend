using BussinessLayer.Repository.ROtros;
using DataLayer.PDbContex;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaCitaConfiguracionRepository : GenericRepository<CtaCitaConfiguracion>, ICtaCitaConfiguracionRepository
    {
        public CtaCitaConfiguracionRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
