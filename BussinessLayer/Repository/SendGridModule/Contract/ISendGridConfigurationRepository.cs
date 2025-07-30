using DataLayer.Models.SendGridModule;

namespace BussinessLayer.Repository.SendGridModule.Contract
{
    public interface ISendGridConfigurationRepository
    {
        Task<SendGridConfiguration> AddAsync(SendGridConfiguration sendGridConfiguration);
        Task<List<SendGridConfiguration>> GetAllAsync(int bussinesId);
        Task<SendGridConfiguration> GetByIdAsync(int configurationId);
        Task DeleteAsync(int configurationId, string userModified);
        Task UpdateAsync(SendGridConfiguration sendGridConfiguration);
    }
}