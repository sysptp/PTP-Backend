using BussinessLayer.Repository.MessagingModule.Contracts;
using DataLayer.Models.MessagingModule;
using DataLayer.PDbContex;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Repository.MessagingModule.Implementations
{
    public class MessagingLogRepository(PDbContext dbContext ) : IMessagingLogRepository
    {
        public async Task AddAsync(MessagingLogs messagingLogs)
        {
            messagingLogs.FechaAdicion = DateTime.UtcNow;
            await dbContext.MessagingLogs.AddAsync(messagingLogs);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<MessagingLogs>> GetAllAsync(int bussinesId)
        {
            return await dbContext.MessagingLogs
            .Where(x => x.BussinesId == bussinesId)
            .ToListAsync();
        }
    }
}
