using BussinessLayer.Interfaces.IBancos;
using DataLayer.Models.Bancos;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SBancos
{
    public class TipoMovimientoBancoService : ITipoMovimientoBancoService
    {
        // CREADO POR MANUEL 17/10/2024
        private readonly PDbContext _context;

        public TipoMovimientoBancoService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(TipoMovimientoBanco model)
        {
            _context.TipoMovimientoBancoes.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TipoMovimientoBanco model)
        {
            _context.TipoMovimientoBancoes.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TipoMovimientoBanco>> GetAll()
        {
            return await _context.TipoMovimientoBancoes.ToListAsync();
        }

        public async Task<List<TipoMovimientoBanco>> GetAllIndex()
        {
            var data = _context.TipoMovimientoBancoes.Include(t => t.empresa).Include(t => t.usuario);

            return await data.ToListAsync();
        }

        public async Task<TipoMovimientoBanco> GetById(int id)
        {
            var result = await _context.TipoMovimientoBancoes.FindAsync(id);

            return result;
        }

        public async Task Update(TipoMovimientoBanco model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
