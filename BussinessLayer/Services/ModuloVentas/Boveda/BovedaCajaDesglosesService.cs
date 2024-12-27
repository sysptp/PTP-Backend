using BussinessLayer.Interfaces.Services.ModuloVentas.IBoveda;
using DataLayer.Models.ModuloVentas.Boveda;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloVentas.Boveda
{
    public class BovedaCajaDesglosesService : IBovedaCajaDesglosesService
    {
        private readonly PDbContext _context;

        public BovedaCajaDesglosesService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(BovedaCajaDesglose model)
        {
            _context.BovedaCajaDesgloses.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(BovedaCajaDesglose model)
        {
            _context.BovedaCajaDesgloses.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BovedaCajaDesglose>> GetAll()
        {
            return await _context.BovedaCajaDesgloses.ToListAsync();
        }

        public async Task<BovedaCajaDesglose> GetById(int id)
        {
            var result = await _context.BovedaCajaDesgloses.FindAsync(id);

            return result;
        }

        public async Task Update(BovedaCajaDesglose model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
