using Twilio.Rest.Api.V2010.Account;

public interface ITwilioService
{
    MessageResource SendMessage(string authToken, string accountSid, string fromNumber, string toNumber,MessageType messageType, string message);
}
