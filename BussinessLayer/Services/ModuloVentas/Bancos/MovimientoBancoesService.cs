using BussinessLayer.Interfaces.ModuloVentas.IBancos;
using DataLayer.Models.ModuloVentas.Bancos;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloVentas.Bancos
{
    public class MovimientoBancoesService : IMovimientoBancoesService
    {
        private readonly PDbContext _context;

        public MovimientoBancoesService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(MovimientoBanco model)
        {
            _context.MovimientoBancoes.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(MovimientoBanco model)
        {
            _context.MovimientoBancoes.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MovimientoBanco>> GetAll()
        {
            return await _context.MovimientoBancoes.ToListAsync();
        }

        public async Task<List<MovimientoBanco>> GetAllIndex()
        {
            var data = _context.MovimientoBancoes.Include(m => m.empresa)
                .Include(m => m.sucursal)
                .Include(m => m.usuario);

            return await data.ToListAsync();
        }

        public async Task<MovimientoBanco> GetById(int id)
        {
            var result = await _context.MovimientoBancoes.FindAsync(id);

            return result;
        }

        public async Task Update(MovimientoBanco model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
