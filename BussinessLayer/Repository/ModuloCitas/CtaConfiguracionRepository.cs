using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Modulo_Citas;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.Modulo_Citas
{
    public class CtaConfiguracionRepository : GenericRepository<CtaConfiguration>, ICtaConfiguracionRepository
    {
        public CtaConfiguracionRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
