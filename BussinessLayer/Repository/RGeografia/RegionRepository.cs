using BussinessLayer.Interfaces.IGeografia;
using BussinessLayer.Interfaces.Repository.Geografia;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.Geografia;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RGeografia
{
    public class RegionRepository : GenericRepository<Region>,IRegionRepository
    {
        private readonly PDbContext _context;

        public RegionRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _context = dbContext;
        }

        public async Task<List<Region>> GetAllIndex()
        {
            var regiones = await _context.Regiones.Include(r => r.Pais).ToListAsync();
            return regiones;
        }
    }
}
