using BussinessLayer.Interfaces.IGeografia;
using DataLayer.Models.Geografia;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SGeografia
{
    public class ProvinciasService : IProvinciaService
    {
        private readonly PDbContext _context;

        public ProvinciasService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Provincia>> GetProvinciasByRegionId(int regionId)
        {

            return await _context.Provincias.Where(x => x.IdRegion == regionId).ToListAsync();
        }

        public async Task<IList<Provincia>> GetAllByEmp(long idEMpresa)
        {

            return await _context.Provincias.Where(x => x.Borrado != true && x.IdEmpresa == idEMpresa).ToListAsync();

        }

        public async Task Add(Provincia model)
        {
            _context.Provincias.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Provincia model)
        {
            _context.Provincias.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Provincia>> GetAll()
        {
            return await _context.Provincias.ToListAsync();
        }

        public async Task<Provincia> GetById(int id)
        {
            var result = await _context.Provincias.FindAsync(id);

            return result;
        }

        public async Task Update(Provincia model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public Task<List<Provincia>> GetAllIndex()
        {
            var provincias = _context.Provincias.Include(p => p.Region).ToListAsync();
            return provincias;  
        }
    }
}
