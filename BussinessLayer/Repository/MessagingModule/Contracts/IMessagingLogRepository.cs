using DataLayer.Models.MessagingModule;

namespace BussinessLayer.Repository.MessagingModule.Contracts
{
    public interface IMessagingLogRepository
    {
        Task AddAsync(MessagingLogs messagingLogs);
        Task<List<MessagingLogs>> GetAllAsync(int bussinesId);
    }
}
