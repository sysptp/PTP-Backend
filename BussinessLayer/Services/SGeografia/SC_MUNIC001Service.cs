using BussinessLayer.Interfaces.IGeografia;
using DataLayer.Models.Geografia;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SGeografia
{
    public class SC_MUNIC001Service : ISC_MUNIC001Service
    {
        private readonly PDbContext _context;

        public SC_MUNIC001Service(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(SC_MUNIC001 model)
        {
            _context.SC_MUNIC001.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SC_MUNIC001 model)
        {
            _context.SC_MUNIC001.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SC_MUNIC001>> GetAll()
        {
            return await _context.SC_MUNIC001.ToListAsync();
        }

        public async Task<List<SC_MUNIC001>> GetAllIndex()
        {
            var sC_MUNIC001 = await _context.SC_MUNIC001.Include(s => s.SC_PROV001).ToListAsync();

            return sC_MUNIC001;
        }

        public async Task<SC_MUNIC001> GetById(int id)
        {
            var result = await _context.SC_MUNIC001.FindAsync(id);

            return result;
        }

        public async Task Update(SC_MUNIC001 model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
