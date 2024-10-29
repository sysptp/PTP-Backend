using BussinessLayer.Interfaces.IGeografia;
using DataLayer.Models.Geografia;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SGeografia
{
    public class SC_PROV001Service : ISC_PROV001Service
    {
        private readonly PDbContext _context;

        public SC_PROV001Service(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(SC_PROV001 model)
        {
            _context.SC_PROV001.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SC_PROV001 model)
        {
            _context.SC_PROV001.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SC_PROV001>> GetAll()
        {
            return await _context.SC_PROV001.ToListAsync();
        }

        public async Task<List<SC_PROV001>> GetAllIndex()
        {
            var sC_PROV001 = await _context.SC_PROV001.Include(s => s.SC_REG001).ToListAsync();
            return sC_PROV001;
        }

        public async Task<SC_PROV001> GetById(int id)
        {
            var result = await _context.SC_PROV001.FindAsync(id);

            return result;
        }

        public async Task Update(SC_PROV001 model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
