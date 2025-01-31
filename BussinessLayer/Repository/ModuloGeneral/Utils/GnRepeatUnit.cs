using BussinessLayer.Interface.Repository.ModuloGeneral;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloGeneral
{
    public class GnRepeatUnitRepository : GenericRepository<GnRepeatUnit>, IGnRepeatUnitRepository
    {
        public GnRepeatUnitRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}