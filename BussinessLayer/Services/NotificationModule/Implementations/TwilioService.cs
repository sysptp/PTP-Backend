using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

public class TwilioService : ITwilioService
{
    public MessageResource SendMessage(string authToken, string accountSid, string fromNumber, string toNumber,MessageType messageType, string message)
    {
        try
        {
            //Inicializando el cliente de Twilio
            TwilioClient.Init(accountSid, authToken);

            //Creando el mensaje
            var messageOptions = new CreateMessageOptions(
                new PhoneNumber(messageType == MessageType.WhatsApp ? "whatsapp:" + toNumber : toNumber));
                messageOptions.From = new PhoneNumber(messageType == MessageType.WhatsApp ? "whatsapp:" + fromNumber : fromNumber);
                messageOptions.Body = message;

                //Enviando el mensaje
                var messageResponse = MessageResource.Create(messageOptions);
                
            //Retornando el resultado
            return messageResponse;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);

        }
}
}