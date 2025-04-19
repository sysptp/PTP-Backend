using BussinessLayer.Interfaces.Repository.ModuloGeneral.Utilis;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.Models.ModuloGeneral.Utils;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloGeneral.Utils
{
    public class GnMessageTypeRepository : GenericRepository<GnMessageType>, IGnMessageTypeRepository
    {
        public GnMessageTypeRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}
