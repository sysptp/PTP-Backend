using BussinessLayer.DTOs.NotificationModule.MessagingConfiguration;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Services.WhatsAppService.Contracts{

    public interface IMessagingConfigurationService
    {
        Task<Response<CreateMessagingConfigurationDto>> CreateAsync(CreateMessagingConfigurationDto request);
        Task<Response<List<MessagingConfigurationDto>>> GetAllAsync(int bussinessId);
        Task<Response<MessagingConfigurationDto>> GetByIdAsync(int configurationId, int businessId);
    }

}
    

    
