using BussinessLayer.Interfaces.ModuloCampaña;
using DataLayer.Models.ModuloCampaña;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RCampaña
{
    public class CmpClienteRepository(PDbContext context) : ICmpClienteRepository
    {
        public async Task<IEnumerable<CmpCliente>> GetAllAsync(long empresaId)
        {
            return await context.CmpClientes.Where(c => !c.Borrado
            && c.EmpresaId == empresaId)
                .ToListAsync();
        }
        public async Task<CmpCliente?> GetByIdAsync(int id, long empresaId)
        {
            return await context.CmpClientes.FirstOrDefaultAsync(c => c.ClientId == id
            && !c.Borrado
            && c.EmpresaId == empresaId);
        }
        public async Task AddAsync(CmpCliente cliente)
        {
            try
            {
                cliente.FechaCreacion = DateTime.UtcNow;
                cliente.FechaModificacion = DateTime.UtcNow;
                context.CmpClientes.Add(cliente);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }
        public async Task UpdateAsync(CmpCliente cliente)
        {
            var existing = await context.Set<CmpCliente>().FindAsync(cliente.ClientId);
            if (existing != null)
            {
                existing.Nombre = cliente.Nombre;
                existing.Estado = cliente.Estado;
                existing.FechaModificacion = DateTime.UtcNow;
                existing.UsuarioModificacion = cliente.UsuarioModificacion;
                existing.Borrado = cliente.Borrado;
                existing.EmpresaId = cliente.EmpresaId;
                context.Update(existing);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(long id)
        {
            var cliente = await context.CmpClientes.FindAsync(id);
            if (cliente != null)
            {
                cliente.Borrado = true;
                cliente.FechaModificacion = DateTime.UtcNow;
                context.Update(cliente);
                await context.SaveChangesAsync();
            }
        }
    }
}
