using BussinessLayer.Repository.MessagingModule.Contracts;
using BussinessLayer.Services.NotificationModule.Contracts;
using DataLayer.Models.MessagingModule;

namespace BussinessLayer.Services.NotificationModule.Implementations
{
    public class MessagingLogService(IMessagingLogRepository messagingLogRepository) : IMessagingLogService
    {
        public async Task AddAsync(CreateMessagingLogDto createMessagingLogDto)
        {
            try
            {
                MessagingLogs messagingLog = new()
                {
                    FromPhoneNumber = createMessagingLogDto.FromPhoneNumber,
                    ToPhoneNumber = createMessagingLogDto.ToPhoneNumber,
                    MessageContent = createMessagingLogDto.MessageContent,
                    MessageReponse = createMessagingLogDto.MessageReponse,
                    BussinesId = createMessagingLogDto.BussinesId
                };
                await messagingLogRepository.AddAsync(messagingLog);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
