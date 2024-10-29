using BussinessLayer.Interfaces.IGeografia;
using DataLayer.Models.Geografia;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SGeografia
{
    public class SC_REG001Service : ISC_REG001Service
    {
        private readonly PDbContext _context;

        public SC_REG001Service(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(SC_REG001 model)
        {
            _context.SC_REG001.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SC_REG001 model)
        {
            _context.SC_REG001.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SC_REG001>> GetAll()
        {
            return await _context.SC_REG001.ToListAsync();
        }

        public async Task<List<SC_REG001>> GetAllIndex()
        {
            var query = _context.SC_REG001
                .Include(s => s.SC_PAIS001);

            var sC_REG001 = await query.ToListAsync();
            return sC_REG001;
        }

        public async Task<List<SC_REG001>> GetAllWithCountry()
        {
            return await _context.SC_REG001
                .Include(x => x.SC_PAIS001)
                .ToListAsync();
        }

        public async Task<SC_REG001> GetById(int id)
        {
            var result = await _context.SC_REG001.FindAsync(id);

            return result;
        }

        public async Task Update(SC_REG001 model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
