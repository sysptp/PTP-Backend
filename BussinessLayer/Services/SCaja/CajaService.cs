using BussinessLayer.Interfaces.ICaja;
using DataLayer.Models.Caja;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SCaja
{
    public class CajaService : ICajaService
    {
        private readonly PDbContext _context;

        public CajaService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Caja model)
        {
            _context.Cajas.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Caja model)
        {
            _context.Cajas.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Caja>> GetAll()
        {
            return await _context.Cajas.ToListAsync();
        }

        public async Task<Caja> GetById(int id)
        {
            var result = await _context.Cajas.FindAsync(id);

            return result;
        }

        public async Task Update(Caja model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
