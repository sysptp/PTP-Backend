using BussinessLayer.Interfaces.ModuloCampaña;
using DataLayer.Models.ModuloCampaña;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RCampaña
{
    public class CmpTipoContactoRepository : ICmpTipoContactoRepository
    {
        private readonly PDbContext _context;
        public CmpTipoContactoRepository(PDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CmpTipoContacto>> GetAllAsync(int idEmpresa)
        {
            return await _context.CmpTipoContactos
                .Where(tc => !tc.Borrado && tc.EmpresaId == idEmpresa)
                .ToListAsync();
        }

        public async Task<CmpTipoContacto?> GetByIdAsync(int id, int idEmpresa)
        {
            return await _context.CmpTipoContactos
                .FirstOrDefaultAsync(tc => tc.TipoContactoId == id && !tc.Borrado && tc.EmpresaId == idEmpresa);
        }

        public async Task AddAsync(CmpTipoContacto tipoContacto)
        {
            tipoContacto.FechaCreacion = DateTime.UtcNow;
            tipoContacto.FechaModificacion = DateTime.UtcNow;
            _context.CmpTipoContactos.Add(tipoContacto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CmpTipoContacto tipoContacto)
        {
            var existing = await _context.CmpTipoContactos.FindAsync(tipoContacto.TipoContactoId);
            if (existing != null)
            {
                existing.Descripcion = tipoContacto.Descripcion;
                existing.FechaModificacion = DateTime.UtcNow;
                existing.UsuarioModificacion = tipoContacto.UsuarioModificacion;
                existing.Borrado = tipoContacto.Borrado;
                existing.EmpresaId = tipoContacto.EmpresaId;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var tipoContacto = await _context.CmpTipoContactos.FindAsync(id);
            if (tipoContacto != null)
            {
                tipoContacto.Borrado = true;
                tipoContacto.FechaModificacion = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
