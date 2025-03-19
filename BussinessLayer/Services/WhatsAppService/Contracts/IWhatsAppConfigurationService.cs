using BussinessLayer.DTOs.WhatsAppModule.WhatsAppConfiguration;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Services.WhatsAppService.Contracts{

    public interface IWhatsAppConfigurationService
    {
        Task<Response<WhatsAppConfigurationDto>> CreateWhatsAppConfiguration(WhatsAppConfigurationDto request);
        Task<Response<List<WhatsAppConfigurationDto>>> GetAllWhatsAppConfigurations(int bussinessId);
        Task<Response<WhatsAppConfigurationDto>> GetWhatsAppConfiguration(int configurationId, int businessId);
    }

}
    

    
