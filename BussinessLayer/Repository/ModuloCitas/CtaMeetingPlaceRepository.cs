using BussinessLayer.Repository.ROtros;
using DataLayer.PDbContex;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaMeetingPlaceRepository : GenericRepository<CtaMeetingPlace>, ICtaMeetingPlaceRepository
    {
        public CtaMeetingPlaceRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
