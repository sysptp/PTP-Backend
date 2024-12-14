using BussinessLayer.Interfaces.ModuloVentas.IBoveda;
using DataLayer.Models.ModuloVentas.Boveda;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloVentas.Boveda
{
    public class BovedaCajasService : IBovedaCajasService
    {
        private readonly PDbContext _context;

        public BovedaCajasService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(BovedaCaja model)
        {
            _context.BovedaCajas.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(BovedaCaja model)
        {
            _context.BovedaCajas.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BovedaCaja>> GetAll()
        {
            return await _context.BovedaCajas.ToListAsync();
        }

        public async Task<BovedaCaja> GetById(int id)
        {
            var result = await _context.BovedaCajas.FindAsync(id);

            return result;
        }

        public async Task Update(BovedaCaja model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
