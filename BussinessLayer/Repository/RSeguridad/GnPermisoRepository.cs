using BussinessLayer.Interfaces.Repository.Seguridad;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Seguridad;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.RSeguridad
{
    public class GnPermisoRepository : GenericRepository<GnPermiso>, IGnPermisoRepository
    {
        public GnPermisoRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
