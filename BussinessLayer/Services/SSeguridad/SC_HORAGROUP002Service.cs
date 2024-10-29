using BussinessLayer.Interfaces.ISeguridad;
using DataLayer.Models.Seguridad;
using DataLayer.PDbContex;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BussinessLayer.Services.SSeguridad
{
    public class SC_HORAGROUP002Service : ISC_HORAGROUP002Service
    {
        private readonly PDbContext _context;

        public SC_HORAGROUP002Service(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(SC_HORAGROUP002 model)
        {
            _context.SC_HORAGROUP002.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SC_HORAGROUP002 model)
        {
            _context.SC_HORAGROUP002.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SC_HORAGROUP002>> GetAll()
        {
            return await _context.SC_HORAGROUP002.ToListAsync();
        }

        public async Task<List<SC_HORAGROUP002>> GetAllIndex()
        {
            var data = _context.SC_HORAGROUP002.Include(s => s.SC_EMP001);

            return await data.ToListAsync();
        }

        public async Task<SC_HORAGROUP002> GetById(int id)
        {
            var result = await _context.SC_HORAGROUP002.FindAsync(id);

            return result;
        }

        public async Task Update(SC_HORAGROUP002 model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
