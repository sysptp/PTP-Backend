using BussinessLayer.DTOs.NotificationModule.MessagingConfiguration;
using BussinessLayer.DTOs.NotificationModule.Twilio;
using BussinessLayer.Services.NotificationModule.Contracts;
using BussinessLayer.Services.WhatsAppService.Contracts;
using BussinessLayer.Wrappers;
using Twilio.Rest.Api.V2010.Account;

namespace BussinessLayer.Services.NotificationModule.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly ITwilioService _twilioService;
        private readonly IMessagingLogService _logService;
        private readonly IMessagingConfigurationService _configurationService;

        public MessageService(ITwilioService twilioService,
            IMessagingLogService logService,
            IMessagingConfigurationService configurationService)
        {
            _twilioService = twilioService;
            _logService = logService;
            _configurationService = configurationService;
        }

        public async Task<Response<MessageResource>> SendMessage(SendMessageDto sendMessage)
        {
            try
            {
                //Obtener la configuracion de Twilio.
                Response<MessagingConfigurationDto> messagingConfiguration = await _configurationService.GetByIdAsync(sendMessage.ConfigurationId, sendMessage.BusinessId);

                //En caso de que sea un error.
                if (!messagingConfiguration.Succeeded) return Response<MessageResource>.BadRequest(messagingConfiguration.Errors);

                if (messagingConfiguration.Data != null)
                {

                    //Crear Dto para enviarlo al servicio de Twilio.
                    SendMessageTwilioDto twilioDto = new SendMessageTwilioDto(
                        messagingConfiguration.Data.AuthToken,
                        messagingConfiguration.Data.AccountSid,
                        sendMessage.MessageType == MessageType.SMS ? messagingConfiguration.Data.SmsNumber : messagingConfiguration.Data.WhatsAppNumber,
                        sendMessage.ToNumber.Trim(),
                        sendMessage.MessageType,
                        sendMessage.Message,
                        sendMessage.BusinessId
                        );

                    MessageResource response = _twilioService.SendMessage(twilioDto);

                    await LogMessage(twilioDto, response);

                    return Response<MessageResource>.Success(response);
                }

                return Response<MessageResource>.BadRequest(new List<string> { $"No existe la configuracion de Twilio con el ID: {sendMessage.ConfigurationId}" });
            }
            catch (Exception ex)
            {
                return Response<MessageResource>.ServerError(ex.Message);
            }
        }


        private async Task LogMessage(SendMessageTwilioDto sendMessageDto, MessageResource messageResponse)
        {
            CreateMessagingLogDto createMessagingLogDto = new CreateMessagingLogDto(
                sendMessageDto.FromNumber,
                sendMessageDto.ToNumber,
                sendMessageDto.Message,
                messageResponse.ErrorCode != null ? messageResponse.ErrorCode.ToString() + " " + messageResponse.ErrorMessage : "Enviado correctamente",
                sendMessageDto.BusinessId
            );

            await _logService.AddAsync(createMessagingLogDto);
        }
    }
}
