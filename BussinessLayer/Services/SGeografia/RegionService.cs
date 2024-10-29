using BussinessLayer.Interfaces.IGeografia;
using DataLayer.Models.Geografia;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SGeografia
{
    public class RegionService : IRegionService
    {
        private readonly PDbContext _context;

        public RegionService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Update(Region model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Add(Region entity)
        {
            try
            {
                _context.Regiones.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }     
        }

        public async Task<Region> GetById(int id)
        {
            return await _context.Regiones.FindAsync(id);
                   
        }

        public async Task<List<Region>> GetAll()
        {
            return await _context.Regiones.ToListAsync();
        }

        public async Task<IList<Region>> GetAllByEmp(long idEMpresa)
        {
            try
            {
                return await _context.Regiones.Where(x => x.Borrado != true && x.IdEmpresa == idEMpresa).ToListAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }         
        }

        public async Task Delete(Region model)
        {
            _context.Regiones.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Region>> GetAllIndex()
        {
            var regiones = await _context.Regiones.Include(r => r.Pais).ToListAsync();
            return regiones;
        }
    }
}
