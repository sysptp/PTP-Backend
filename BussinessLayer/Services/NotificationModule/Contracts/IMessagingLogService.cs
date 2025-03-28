namespace BussinessLayer.Services.NotificationModule.Contracts
{
    public interface IMessagingLogService
    {
        Task AddAsync(CreateMessagingLogDto createMessagingLogDto);
    }
}
