using BussinessLayer.Interfaces.ISeguridad;
using DataLayer.Models.Seguridad;
using DataLayer.PDbContex;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BussinessLayer.Services.SSeguridad
{
    public class SC_USUAR001Service : ISC_USUAR001Service
    {
        // CREADO POR MANUEL 17/10/2024
        private readonly PDbContext _context;

        public SC_USUAR001Service(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(SC_USUAR001 model)
        {
            _context.SC_USUAR001.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SC_USUAR001 model)
        {
            _context.SC_USUAR001.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SC_USUAR001>> GetAll()
        {
            return await _context.SC_USUAR001.ToListAsync();
        }

        public async Task<List<SC_USUAR001>> GetAllWithEmpId(long id)
        {
            var data = await _context.SC_USUAR001.Where(x => x.CODIGO_EMP == id).Include(s => s.SC_EMP001).ToListAsync();
            return data;   
        }

        public async Task<SC_USUAR001> GetById(int id)
        {
            var result = await _context.SC_USUAR001.FindAsync(id);

            return result;
        }

        public async Task Update(SC_USUAR001 model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
