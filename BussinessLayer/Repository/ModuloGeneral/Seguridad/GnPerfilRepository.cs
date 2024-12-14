using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloGeneral.Seguridad
{
    public class GnPerfilRepository : GenericRepository<GnPerfil>, IGnPerfilRepository
    {
        public GnPerfilRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

    }
}
