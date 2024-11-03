using BussinessLayer.Interfaces.Repository.Configuracion.Menu;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.MenuApp;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.RConfiguracion.Menu
{
    public class GnMenuRepository : GenericRepository<GnMenu>, IGnMenuRepository
    {
        public GnMenuRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

        
    }
}
