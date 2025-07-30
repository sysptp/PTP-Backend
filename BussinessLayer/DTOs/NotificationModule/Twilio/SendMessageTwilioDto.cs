namespace BussinessLayer.DTOs.NotificationModule.Twilio
{
    public record SendMessageTwilioDto
    (
     string AuthToken,
     string AccountSid,
     string FromNumber,
     string ToNumber,
     MessageType MessageType,
     string Message,
     long BusinessId
    );


}
