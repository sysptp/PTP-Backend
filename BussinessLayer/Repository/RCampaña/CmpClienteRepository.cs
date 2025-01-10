using BussinessLayer.Interfaces.ModuloCampaña;
using DataLayer.Models.ModuloCampaña;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RCampaña
{
    public class CmpClienteRepository : ICmpClienteRepository
    {
        private readonly PDbContext _context;

        public CmpClienteRepository(PDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CmpCliente>> GetAllAsync(int empresaId)
        {
            return await _context.Set<CmpCliente>().Where(c => !c.Borrado && c.EmpresaId == empresaId).ToListAsync();
        }
        public async Task<CmpCliente?> GetByIdAsync(int id, int empresaId)
        {
            return await _context.Set<CmpCliente>().FirstOrDefaultAsync(c => c.ClientId == id && !c.Borrado && c.EmpresaId == empresaId);
        }
        public async Task AddAsync(CmpCliente cliente)
        {
            try
            {
                cliente.FechaCreacion = DateTime.UtcNow;
                cliente.FechaModificacion = DateTime.UtcNow;
                _context.CmpClientes.Add(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }

        }
        public async Task UpdateAsync(CmpCliente cliente)
        {
            var existing = await _context.Set<CmpCliente>().FindAsync(cliente.ClientId);
            if (existing != null)
            {
                existing.Nombre = cliente.Nombre;
                existing.Estado = cliente.Estado;
                existing.FechaModificacion = DateTime.UtcNow;
                existing.UsuarioModificacion = cliente.UsuarioModificacion;
                existing.Borrado = cliente.Borrado;
                existing.EmpresaId = cliente.EmpresaId;
                _context.Update(existing);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int id)
        {
            var cliente = await _context.CmpClientes.FindAsync(id);
            if (cliente != null)
            {
                cliente.Borrado = true;
                cliente.FechaModificacion = DateTime.UtcNow;
                _context.Update(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
