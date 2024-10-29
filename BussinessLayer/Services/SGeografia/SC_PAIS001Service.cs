using BussinessLayer.Interfaces.IGeografia;
using DataLayer.Models.Geografia;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SGeografia
{
    public class SC_PAIS001Service : ISC_PAIS001Service
    {
        private readonly PDbContext _context;

        public SC_PAIS001Service(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(SC_PAIS001 model)
        {
            _context.SC_PAIS001.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SC_PAIS001 model)
        {
            _context.SC_PAIS001.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SC_PAIS001>> GetAll()
        {
            return await _context.SC_PAIS001.ToListAsync();
        }

        public async Task<SC_PAIS001> GetById(int id)
        {
            var result = await _context.SC_PAIS001.FindAsync(id);

            return result;
        }

        public async Task Update(SC_PAIS001 model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
