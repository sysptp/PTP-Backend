using BussinessLayer.DTOs.NotificationModule.MessagingConfiguration;
using BussinessLayer.Wrappers;
using Twilio.Rest.Api.V2010.Account;

namespace BussinessLayer.Services.NotificationModule.Contracts
{
    public interface IMessageService
    {
        Task<Response<MessageResource>> SendMessage(SendMessageDto sendMessage);
    }
}
