//using BussinessLayer.DTOs.NotificationModule.Twilio;
//using BussinessLayer.Services.NotificationModule.Contracts;
//using Twilio;
//using Twilio.Rest.Api.V2010.Account;
//using Twilio.Types;

//namespace BussinessLayer.Services.NotificationModule.Implementations
//{
//    public class TwilioService(IMessagingLogService messagingLogService) : ITwilioService
//    {
//        public async Task<MessageResource> SendMessage(SendMessageTwilioDto sendMessageDto)
//        {
//            try
//            {
//                //Inicializando el cliente de Twilio
//                TwilioClient.Init(sendMessageDto.AccountSid, sendMessageDto.AuthToken);

//                //Creando el mensaje
//                var messageOptions = new CreateMessageOptions(
//                    new PhoneNumber(sendMessageDto.MessageType == MessageType.WhatsApp ? "whatsapp:" + sendMessageDto.ToNumber : sendMessageDto.ToNumber));
//                messageOptions.From = new PhoneNumber(sendMessageDto.MessageType == MessageType.WhatsApp ? "whatsapp:" + sendMessageDto.FromNumber : sendMessageDto.FromNumber);
//                messageOptions.Body = sendMessageDto.Message;

//                //Enviando el mensaje
//                var messageResponse = await MessageResource.CreateAsync(messageOptions);

//                await LogMessage(sendMessageDto, messageResponse);

//                //Retornando el resultado
//                return messageResponse;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        public async Task LogMessage(SendMessageTwilioDto sendMessageDto, MessageResource messageResponse)
//        {
//            CreateMessagingLogDto createMessagingLogDto = new CreateMessagingLogDto(
//                sendMessageDto.FromNumber,
//                sendMessageDto.ToNumber,
//                sendMessageDto.Message,
//                messageResponse.ErrorCode != null ? messageResponse.ErrorCode.ToString() + " " + messageResponse.ErrorMessage : "Enviado correctamente",
//                sendMessageDto.BusinessId
//            );
//            await messagingLogService.AddAsync(createMessagingLogDto);
//        }
//    }
//}