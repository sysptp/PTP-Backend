using BussinessLayer.Interfaces.Repository.ModuloGeneral.Geografia;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloGeneral.Geografia;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.ModuloGeneral.Geografia
{
    public class ProvinciaRepository : GenericRepository<Provincia>, IProvinciaRepository
    {
        private readonly PDbContext _context;

        public ProvinciaRepository(PDbContext dbContext, ITokenService tokenService) : base(dbContext, tokenService)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Provincia>> GetProvinciasByRegionId(int regionId)
        {

            return await _context.Provincia.Where(x => x.IdRegion == regionId).ToListAsync();
        }

        public Task<List<Provincia>> GetAllIndex()
        {
            var provincias = _context.Provincia.Include(p => p.Region).ToListAsync();
            return provincias;
        }
    }
}
