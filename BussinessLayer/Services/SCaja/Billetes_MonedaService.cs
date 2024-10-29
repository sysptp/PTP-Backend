using BussinessLayer.Interfaces.ICaja;
using DataLayer.Models.Caja;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SCaja
{
    public class Billetes_MonedaService : IBilletes_MonedaService
    {
        private readonly PDbContext _context;

        public Billetes_MonedaService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Billetes_Moneda model)
        {
            _context.Billetes_Moneda.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Billetes_Moneda model)
        {
            _context.Billetes_Moneda.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Billetes_Moneda>> GetAll()
        {
            return await _context.Billetes_Moneda.ToListAsync();
        }

        public async Task<Billetes_Moneda> GetById(int id)
        {
            var result = await _context.Billetes_Moneda .FindAsync(id);

            return result;
        }

        public async Task Update(Billetes_Moneda model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
