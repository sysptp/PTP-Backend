using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Modulo_Citas;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.Modulo_Citas
{
    public class CtaStateRepository : GenericRepository<CtaState>, ICtaStateRepository
    {
        public CtaStateRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
