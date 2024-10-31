using BussinessLayer.Interfaces.IAutenticacion;
using BussinessLayer.Interfaces.Repository.Seguridad;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Entities;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.RSeguridad
{
    public class GnPerfilRepository : GenericRepository<GnPerfil>, IGnPerfilRepository
    {
        public GnPerfilRepository(PDbContext dbContext, IClaimsService claimsService) : base(dbContext, claimsService)
        {
        }
    }
}
