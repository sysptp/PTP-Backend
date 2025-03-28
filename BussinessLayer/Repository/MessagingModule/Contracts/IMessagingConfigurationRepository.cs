using DataLayer.Models.MessagingModule;


public interface IMessagingConfigurationRepository
{
    Task<List<MessagingConfiguration>> GetAllAsync(int BussinessId);
    Task<MessagingConfiguration> GetByIdAsync(int configurationId,int BussinessId);
    Task<MessagingConfiguration> CreateAsync(MessagingConfiguration configuration);
    Task<MessagingConfiguration> UpdateAsync(MessagingConfiguration configuration);
    Task<MessagingConfiguration> DeleteAsync(int configurationId,int BussinessId);
}
