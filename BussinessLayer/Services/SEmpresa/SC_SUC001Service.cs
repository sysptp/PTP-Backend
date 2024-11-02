using BussinessLayer.Interfaces.IEmpresa;
using DataLayer.Models.Empresa;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SEmpresa
{
    public class SC_SUC001Service : ISC_SUC001Service
    {
        // CREADO POR MANUEL 17/10/2024
        private readonly PDbContext _context;

        public SC_SUC001Service(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(SC_SUC001 model)
        {
            _context.SC_SUC001.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SC_SUC001 model)
        {
            _context.SC_SUC001.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SC_SUC001>> GetAll()
        {
            return await _context.SC_SUC001.ToListAsync();
        }

        public async Task<List<SC_SUC001>> GetAllIndex()
        {
            return await _context.SC_SUC001
                .Include(s => s.GnEmpresa)
                .ToListAsync();
        }

        public async Task<SC_SUC001> GetById(int id)
        {
            var result = await _context.SC_SUC001.FindAsync(id);

            return result;
        }

        public async Task Update(SC_SUC001 model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
