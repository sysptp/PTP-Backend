using BussinessLayer.Interfaces.Repository.ModuloGeneral.Geografia;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.Geografia;
using DataLayer.PDbContex;

namespace BussinessLayer.Repository.ModuloGeneral.Geografia
{
    public class PaisRepository : GenericRepository<Pais>, IPaisRepository
    {
        public PaisRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}