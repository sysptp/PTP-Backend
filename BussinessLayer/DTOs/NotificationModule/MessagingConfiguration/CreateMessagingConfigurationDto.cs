
namespace BussinessLayer.DTOs.NotificationModule.MessagingConfiguration
{
    public record CreateMessagingConfigurationDto(
        long BussinessId,
        string AccountSid,
        string AuthToken,
        string WhatsAppNumber,
        string SmsNumber,
        string AddedBy
    );

}