using BussinessLayer.Interfaces.ICuentas;
using DataLayer.Models.Bancos;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SCuentas
{
    public class CuentaBancosService : ICuentaBancosService
    {
        private readonly PDbContext _context;

        public CuentaBancosService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(CuentaBancos model)
        {
            _context.CuentaBancos.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(CuentaBancos model)
        {
            _context.CuentaBancos.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CuentaBancos>> GetAll()
        {
            return await _context.CuentaBancos.ToListAsync();
        }

        public async Task<List<CuentaBancos>> GetAllIndex()
        {
            var data = _context.CuentaBancos
                .Include(c => c.banco)
                .Include(c => c.empresa)
                .Include(c => c.moneda)
                .Include(c => c.sucursal);

            return await data.ToListAsync();
        }

        public async Task<CuentaBancos> GetById(int id)
        {
            var result = await _context.CuentaBancos.FindAsync(id);

            return result;
        }

        public async Task Update(CuentaBancos model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
