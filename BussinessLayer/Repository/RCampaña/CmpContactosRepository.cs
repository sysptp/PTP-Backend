using BussinessLayer.Interfaces.ModuloCampaña.Repository;
using DataLayer.Models.ModuloCampaña;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RCampaña
{
    public class CmpContactosRepository : ICmpContactosRepository
    {
        private readonly PDbContext _context;

        public CmpContactosRepository(PDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CmpContactos>> GetAllAsync(long empresaId)
        {
            return await _context.CmpContactos.Where(c => !c.Borrado).ToListAsync();
        }

        public async Task<CmpContactos?> GetByIdAsync(long id, long empresaId)
        {
            return await _context.CmpContactos
                .FirstOrDefaultAsync(c => c.ContactoId == id && !c.Borrado);
        }

        public async Task AddAsync(CmpContactos contacto)
        {
            contacto.FechaCreacion = DateTime.UtcNow;
            contacto.FechaModificacion = DateTime.UtcNow;
            _context.Set<CmpContactos>().Add(contacto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CmpContactos contacto)
        {
            var existing = await _context.Set<CmpContactos>().FindAsync(contacto.ContactoId);
            if (existing != null)
            {
                existing.ClienteId = contacto.ClienteId;
                existing.Contacto = contacto.Contacto;
                existing.TipoContactoId = contacto.TipoContactoId;
                existing.Estado = contacto.Estado;
                existing.FechaModificacion = DateTime.UtcNow;
                existing.UsuarioModificacion = contacto.UsuarioModificacion;
                existing.Borrado = contacto.Borrado;
                existing.EmpresaId = contacto.EmpresaId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(long id)
        {
            var contacto = await _context.Set<CmpContactos>().FindAsync(id);
            if (contacto != null)
            {
                contacto.Borrado = true;
                contacto.FechaModificacion = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
