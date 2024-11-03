using BussinessLayer.Interfaces.Repository.Geografia;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Geografia;
using DataLayer.PDbContex;

namespace BussinessLayer.Services.SGeografia
{
    public class PaisRepository : GenericRepository<Pais>, IPaisRepository
    {
        public PaisRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
        }
    }
}