
namespace BussinessLayer.DTOs.NotificationModule.MessagingConfiguration
{
    public record CreateMessagingConfigurationDto(
        int BussinessId,
        string AccountSid,
        string AuthToken,
        string WhatsAppNumber,
        string AddedBy
    );

}