using BussinessLayer.Interfaces.ISeguridad;
using DataLayer.Models.Seguridad;
using DataLayer.PDbContex;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BussinessLayer.Services.SSeguridad
{
    public class SC_HORARIO001Service : ISC_HORARIO001Service
    {
        private readonly PDbContext _context;

        public SC_HORARIO001Service(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(SC_HORARIO001 model)
        {
            _context.SC_HORARIO001.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SC_HORARIO001 model)
        {
            _context.SC_HORARIO001.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SC_HORARIO001>> GetAll()
        {
            return await _context.SC_HORARIO001.ToListAsync();
        }

        public async Task<List<SC_HORARIO001>> GetAllIndex()
        {
            var data = _context.SC_HORARIO001.Include(s => s.SC_EMP001);

            return await data.ToListAsync();
        }

        public async Task<SC_HORARIO001> GetById(int id)
        {
            var result = await _context.SC_HORARIO001.FindAsync(id);

            return result;
        }

        public async Task Update(SC_HORARIO001 model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
