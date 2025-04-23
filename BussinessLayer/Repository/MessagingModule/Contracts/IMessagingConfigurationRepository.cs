using DataLayer.Models.MessagingModule;


public interface IMessagingConfigurationRepository
{
    Task<List<MessagingConfiguration>> GetAllAsync(long BussinessId);
    Task<MessagingConfiguration> GetByIdAsync(int configurationId,long BussinessId);
    Task<MessagingConfiguration> CreateAsync(MessagingConfiguration configuration);
    Task<MessagingConfiguration> UpdateAsync(MessagingConfiguration configuration);
    Task<MessagingConfiguration> DeleteAsync(int configurationId,long BussinessId);
    MessagingConfiguration GetByCompanyIdAsync(long BussinessId);
}
