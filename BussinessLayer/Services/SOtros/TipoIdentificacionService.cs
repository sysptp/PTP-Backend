using BussinessLayer.Interface.IOtros;
using DataLayer.Models.Otros;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SOtros
{
    public class TipoIdentificacionService : ITipoIdentificacionService
    {
        private readonly PDbContext _context;

        public TipoIdentificacionService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Tipo_Identificacion model)
        {
            _context.Tipo_Identificacion.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Tipo_Identificacion model)
        {
            _context.Tipo_Identificacion.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Tipo_Identificacion>> GetAll()
        {
            return await _context.Tipo_Identificacion.ToListAsync();
        }

        public async Task<Tipo_Identificacion> GetById(int id)
        {
            var result = await _context.Tipo_Identificacion.FindAsync(id);

            return result;
        }

        public async Task Update(Tipo_Identificacion model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
