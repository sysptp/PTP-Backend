using BussinessLayer.Interfaces.Repository.ModuloGeneral.Geografia;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.Geografia;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ModuloGeneral.Geografia
{
    public class RegionRepository : GenericRepository<Region>, IRegionRepository
    {
        private readonly PDbContext _context;

        public RegionRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _context = dbContext;
        }

        public async Task<List<Region>> GetAllIndex()
        {
            var regiones = await _context.Region.Include(r => r.Pais).ToListAsync();
            return regiones;
        }
    }
}
