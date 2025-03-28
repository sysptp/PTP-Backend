using Twilio.Rest.Api.V2010.Account;

public interface ITwilioService
{
    Task<MessageResource> SendMessage(SendMessageDto sendMessageDto);
}
