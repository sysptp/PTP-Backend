using BussinessLayer.Interfaces.Repository.ModuloGeneral.Modulo;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.Menu;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloGeneral.Modulo
{
    public class GnModuloRepository : GenericRepository<GnModulo>, IGnModuloRepository
    {
        public GnModuloRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

    }
}
