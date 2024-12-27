using BussinessLayer.Interfaces.Services.ModuloVentas.IBoveda;
using DataLayer.Models.ModuloVentas.Boveda;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloVentas.Boveda
{
    public class BovedaMovimientoesService : IBovedaMovimientoesService
    {
        private readonly PDbContext _context;

        public BovedaMovimientoesService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(BovedaMovimiento model)
        {
            _context.BovedaMovimientoes.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(BovedaMovimiento model)
        {
            _context.BovedaMovimientoes.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BovedaMovimiento>> GetAll()
        {
            return await _context.BovedaMovimientoes.ToListAsync();
        }

        public async Task<BovedaMovimiento> GetById(int id)
        {
            var result = await _context.BovedaMovimientoes.FindAsync(id);

            return result;
        }

        public async Task Update(BovedaMovimiento model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
