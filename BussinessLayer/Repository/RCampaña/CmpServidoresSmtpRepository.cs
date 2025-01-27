// Repositorio e interfaz
using BussinessLayer.Interfaces.ModuloCampaña;
using DataLayer.Models.ModuloCampaña;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.RCampaña
{
    public class CmpServidoresSmtpRepository : ICmpServidoresSmtpRepository
    {
        private readonly PDbContext _context;

        public CmpServidoresSmtpRepository(PDbContext context)
        {
            _context = context;
        }

        public async Task<List<CmpServidoresSmtp>> GetAllAsync()
        {
            try
            {
                return await _context.CmpServidoresSmtps.Where(s => !s.Borrado).ToListAsync();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception($"Error al obtener todos los servidores SMTP: {ex.Message}", ex);
            }
        }

        public async Task<CmpServidoresSmtp?> GetByIdAsync(long id)
        {
            try
            {
                return await _context.CmpServidoresSmtps
                    .FirstOrDefaultAsync(s => s.ServidorId == id && !s.Borrado);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception($"Error al obtener el servidor SMTP con ID {id}: {ex.Message}", ex);
            }
        }

        public async Task AddAsync(CmpServidoresSmtp servidor)
        {
            try
            {
                servidor.FechaAdicion = DateTime.UtcNow;
                servidor.FechaModificacion = DateTime.UtcNow;
                _context.Set<CmpServidoresSmtp>().Add(servidor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception($"Error al agregar el servidor SMTP: {ex.Message}", ex);
            }
        }

        public async Task UpdateAsync(CmpServidoresSmtp servidor)
        {
            try
            {
                var existing = await _context.Set<CmpServidoresSmtp>().FindAsync(servidor.ServidorId);
                if (existing != null)
                {
                    existing.Nombre = servidor.Nombre;
                    existing.Host = servidor.Host;
                    existing.Puerto = servidor.Puerto;
                    existing.FechaModificacion = DateTime.UtcNow;
                    existing.Borrado = servidor.Borrado;

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception($"Error al actualizar el servidor SMTP con ID {servidor.ServidorId}: {ex.Message}", ex);
            }
        }

        public async Task DeleteAsync(long id)
        {
            try
            {
                var servidor = await _context.Set<CmpServidoresSmtp>().FindAsync(id);
                if (servidor != null)
                {
                    servidor.Borrado = true;
                    servidor.FechaModificacion = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception($"Error al eliminar el servidor SMTP con ID {id}: {ex.Message}", ex);
            }
        }
    }
}
