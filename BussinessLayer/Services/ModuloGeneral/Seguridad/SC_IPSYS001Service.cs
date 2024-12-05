using BussinessLayer.Interfaces.ISeguridad;
using DataLayer.Models.Seguridad;
using DataLayer.PDbContex;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BussinessLayer.Services.ModuloGeneral.Seguridad
{
    public class SC_IPSYS001Service : ISC_IPSYS001Service
    {
        private readonly PDbContext _context;

        public SC_IPSYS001Service(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(SC_IPSYS001 model)
        {
            _context.SC_IPSYS001.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SC_IPSYS001 model)
        {
            _context.SC_IPSYS001.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SC_IPSYS001>> GetAll()
        {
            return await _context.SC_IPSYS001.ToListAsync();
        }

        public async Task<List<SC_IPSYS001>> GetAllIndex()
        {
            var data = _context.SC_IPSYS001.Include(s => s.SC_USUAR001);

            return await data.ToListAsync();
        }

        public async Task<SC_IPSYS001> GetById(int id)
        {
            var result = await _context.SC_IPSYS001.FindAsync(id);

            return result;
        }

        public async Task Update(SC_IPSYS001 model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
