using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloGeneral.Seguridad
{
    public class GnPermisoRepository : GenericRepository<GnPermiso>, IGnPermisoRepository
    {
        public GnPermisoRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
