namespace BussinessLayer.Services.WhatsAppService.Contracts
{
    public interface IWhatsAppService
    {
        Task SendMessage(string message, string fromPhoneNumber, string toPhoneNumber);
    }
}
