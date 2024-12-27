using BussinessLayer.Interfaces.Repository.ModuloGeneral.Menu;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.Menu;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloGeneral.Menu
{
    public class GnMenuRepository : GenericRepository<GnMenu>, IGnMenuRepository
    {
        public GnMenuRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }


    }
}
