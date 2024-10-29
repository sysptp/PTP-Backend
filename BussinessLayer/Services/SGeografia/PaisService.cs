using BussinessLayer.Interfaces.IGeografia;
using DataLayer.Models.Geografia;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SGeografia
{
    public class PaisService : IPaisService
    {
        private readonly PDbContext _context;

        public PaisService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Pais entity)
        {
            entity.FechaModificacion = DateTime.Now;
            _context.Paises.Add(entity);

            await _context.SaveChangesAsync();           
        }

        public async Task Edit(Pais entity)
        {
            var oldPais = await _context.Paises.FindAsync(entity.Id);
            if (oldPais == null) return;
            oldPais.Nombre = entity.Nombre;
            oldPais.FechaModificacion = DateTime.Now;
            _context.SaveChanges();   
        }

        public async Task<Pais> GetById(int id)
        {
            return await _context.Paises.FindAsync(id);         
        }

        public async Task<List<Pais>> GetAll()
        {

            return await _context.Paises.Where(x => x.Borrado != true).ToListAsync();        
        }

        public async Task Delete(Pais model)
        {
            if (model == null) return;
            model.Borrado = true;

            await _context.SaveChangesAsync();
        }

        public async Task Update(Pais model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}