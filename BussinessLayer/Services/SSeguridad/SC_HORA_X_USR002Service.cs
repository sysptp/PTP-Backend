using BussinessLayer.Interfaces.ISeguridad;
using DataLayer.Models.Seguridad;
using DataLayer.PDbContex;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BussinessLayer.Services.SSeguridad
{
    public class SC_HORA_X_USR002Service : ISC_HORA_X_USR002Service
    {
        private readonly PDbContext _context;

        public SC_HORA_X_USR002Service(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(SC_HORA_X_USR002 model)
        {
            _context.SC_HORA_X_USR002.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SC_HORA_X_USR002 model)
        {
            _context.SC_HORA_X_USR002.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SC_HORA_X_USR002>> GetAll()
        {
            return await _context.SC_HORA_X_USR002.ToListAsync();
        }

        public async Task<List<SC_HORA_X_USR002>> GetAllIndex()
        {
            var data = _context.SC_HORA_X_USR002.Include(s => s.GnEmpresa);

            return await data.ToListAsync();
        }

        public async Task<SC_HORA_X_USR002> GetById(int id)
        {
            var result = await _context.SC_HORA_X_USR002.FindAsync(id);

            return result;
        }

        public async Task Update(SC_HORA_X_USR002 model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
