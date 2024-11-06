using BussinessLayer.Interfaces.Repository.Configuracion.Modulo;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.MenuApp;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.RConfiguracion.Modulo
{
    public class GnModuloRepository : GenericRepository<GnModulo>, IGnModuloRepository
    {
        public GnModuloRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

    }
}
