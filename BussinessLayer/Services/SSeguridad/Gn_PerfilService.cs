using BussinessLayer.Interfaces.ISeguridad;
using DataLayer.Models.Seguridad;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Services.SSeguridad
{
    public class Gn_PerfilService : IGn_PerfilService
    {
        private readonly PDbContext _context;

        public Gn_PerfilService(PDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task Add(Gn_Perfil model)
        {
            _context.Gn_Perfil.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Gn_Perfil model)
        {
            _context.Gn_Perfil.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Gn_Perfil>> GetAll()
        {
            return await _context.Gn_Perfil.ToListAsync();
        }

        public async Task<Gn_Perfil> GetById(int id)
        {
            var result = await _context.Gn_Perfil.FindAsync(id);

            return result;
        }

        public async Task Update(Gn_Perfil model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
