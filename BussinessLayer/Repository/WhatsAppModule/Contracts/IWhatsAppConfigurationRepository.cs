using DataLayer.Models.WhatsAppFeature;

/*
 * Interfaz que define las operaciones CRUD para la configuración de WhatsApp
 * 
 * GetAllWhatsAppConfigurationAsync: Obtiene todas las configuraciones de WhatsApp por el id de la empresa
 * GetWhatsAppConfigurationAsync: Obtiene una configuración de WhatsApp por su id y el id de la empresa
 * CreateWhatsAppConfigurationAsync: Crea una nueva configuración de WhatsApp
 * UpdateWhatsAppConfigurationAsync: Actualiza una configuración de WhatsApp existente
 * DeleteWhatsAppConfigurationAsync: Elimina una configuración de WhatsApp por su id y el id de la empresa
 */

public interface IWhatsAppConfigurationRepository
{
    Task<List<CmpWhatsAppConfiguration>> GetAllWhatsAppConfigurationAsync(int BussinessId);
    Task<CmpWhatsAppConfiguration> GetWhatsAppConfigurationAsync(int configurationId,int BussinessId);
    Task<CmpWhatsAppConfiguration> CreateWhatsAppConfigurationAsync(CmpWhatsAppConfiguration configuration);
    Task<CmpWhatsAppConfiguration> UpdateWhatsAppConfigurationAsync(CmpWhatsAppConfiguration configuration);
    Task<CmpWhatsAppConfiguration> DeleteWhatsAppConfigurationAsync(int configurationId,int BussinessId);
}
