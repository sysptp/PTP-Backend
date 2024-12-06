using BussinessLayer.Interfaces.Repository.ModuloGeneral.Modulo;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.Menu;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloGeneral.Modulo
{
    public class GnEmpresaXModuloRepository : GenericRepository<GnEmpresaXModulo>, IGnEmpresaXModuloRepository
    {
        public GnEmpresaXModuloRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
