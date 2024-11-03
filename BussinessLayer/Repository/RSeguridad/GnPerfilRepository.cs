using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Entities;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.RSeguridad
{
    public class GnPerfilRepository : GenericRepository<GnPerfil>, IGnPerfilRepository
    {
        public GnPerfilRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }

    }
}
