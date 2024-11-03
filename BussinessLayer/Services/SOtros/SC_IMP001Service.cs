using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Otros;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SOtros
{
    public class SC_IMP001Service : ISC_IMP001Service
    {
        private readonly PDbContext _context;

        public SC_IMP001Service(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(SC_IMP001 model)
        {
            _context.SC_IMP001.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SC_IMP001 model)
        {
            _context.SC_IMP001.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SC_IMP001>> GetAll()
        {
            return await _context.SC_IMP001.ToListAsync();
        }

        public async Task<List<SC_IMP001>> GetAllIndex()
        {
            var data = _context.SC_IMP001.Include(s => s.GnEmpresa);

            return await data.ToListAsync();
        }

        public async Task<SC_IMP001> GetById(int id)
        {
            var result = await _context.SC_IMP001.FindAsync(id);

            return result;
        }

        public async Task Update(SC_IMP001 model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
