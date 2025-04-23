namespace BussinessLayer.DTOs.NotificationModule.MessagingConfiguration
{
    public record SendMessageDto
    (
        int ConfigurationId,
        string ToNumber,
        MessageType MessageType,
        string Message,
        long BusinessId
    );
}