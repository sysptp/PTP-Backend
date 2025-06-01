using BussinessLayer.DTOs.NotificationModule.MessagingConfiguration;
using BussinessLayer.DTOs.NotificationModule.Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BussinessLayer.Services.NotificationModule.Contracts
{
    public interface ITwilioService
    {
        MessageResource SendMessage(SendMessageTwilioDto sendMessageDto);
    }
}