using DataLayer.Models.WhatsAppFeature;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

/*
 * Repositorio que implementa las operaciones CRUD para la configuración de WhatsApp
 * 
 * GetWhatsAppConfigurationAsync: Obtiene una configuración de WhatsApp por su id y el id de la empresa
 * CreateWhatsAppConfigurationAsync: Crea una nueva configuración de WhatsApp
 * UpdateWhatsAppConfigurationAsync: Actualiza una configuración de WhatsApp existente
 * DeleteWhatsAppConfigurationAsync: Elimina una configuración de WhatsApp por su id y el id de la empresa
 
 * @param _context: Contexto de la base de datos para acceder a las entidades
 */
public class WhatsAppConfigurationRepository : IWhatsAppConfigurationRepository
{
    private readonly PDbContext _context;

    public WhatsAppConfigurationRepository(PDbContext context)
    {
        _context = context;
    }
    public async Task<CmpWhatsAppConfiguration> CreateWhatsAppConfigurationAsync(CmpWhatsAppConfiguration configuration)
    {
        try
        {
            configuration.FechaAdicion = DateTime.Now;
            await _context.CmpWhatsAppConfigurations.AddAsync(configuration);
            await _context.SaveChangesAsync();
            return configuration;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CmpWhatsAppConfiguration> DeleteWhatsAppConfigurationAsync(int configurationId, int BussinessId)
    {
        try
        {
            var configuration = await _context.CmpWhatsAppConfigurations.FindAsync(configurationId);
            if (configuration == null)
            {
                throw new Exception("Configuration not found");
            }
            configuration.Borrado = true;
            configuration.FechaModificacion = DateTime.Now;
            _context.CmpWhatsAppConfigurations.Update(configuration);
            await _context.SaveChangesAsync();
            return configuration;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<CmpWhatsAppConfiguration>> GetAllWhatsAppConfigurationAsync(int BussinessId)
    {
        try
        {
            var configurations = await _context.CmpWhatsAppConfigurations
            .Where(c => c.BussinessId == BussinessId && c.Borrado == false)
            .ToListAsync();
            
            return configurations;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CmpWhatsAppConfiguration> GetWhatsAppConfigurationAsync(int configurationId, int BussinessId)
    {
        try
        {
            var configuration = await _context.CmpWhatsAppConfigurations.FindAsync(configurationId);
            if (configuration == null)
            {
                throw new Exception("Configuration not found");
            }
            return configuration;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CmpWhatsAppConfiguration> UpdateWhatsAppConfigurationAsync(CmpWhatsAppConfiguration configuration)
    {
        try
        {
            _context.CmpWhatsAppConfigurations.Update(configuration);
            await _context.SaveChangesAsync();
            return configuration;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
