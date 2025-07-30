using BussinessLayer.DTOs.NotificationModule.MessagingConfiguration;
using BussinessLayer.Wrappers;

namespace BussinessLayer.Services.WhatsAppService.Contracts{

    public interface IMessagingConfigurationService
    {
        Task<Response<CreateMessagingConfigurationDto>> CreateAsync(CreateMessagingConfigurationDto request);
        Task<Response<List<MessagingConfigurationDto>>> GetAllAsync(long bussinessId);
        Task<Response<MessagingConfigurationDto>> GetByIdAsync(int configurationId, long businessId);
    }

}
    

    
