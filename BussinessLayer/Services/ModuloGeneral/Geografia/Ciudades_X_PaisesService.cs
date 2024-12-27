using BussinessLayer.Interfaces.Services.ModuloGeneral.Geografia;
using DataLayer.Models.ModuloGeneral.Geografia;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.ModuloGeneral.Geografia
{
    public class Ciudades_X_PaisesService : ICiudades_X_PaisesService
    {
        private readonly PDbContext _context;

        public Ciudades_X_PaisesService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Ciudades_X_Paises model)
        {
            _context.Ciudades_X_Paises.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Ciudades_X_Paises model)
        {
            _context.Ciudades_X_Paises.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Ciudades_X_Paises>> GetAll()
        {
            return await _context.Ciudades_X_Paises.ToListAsync();
        }

        public async Task<Ciudades_X_Paises> GetById(int id)
        {
            var result = await _context.Ciudades_X_Paises.FindAsync(id);

            return result;
        }

        public async Task Update(Ciudades_X_Paises model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
