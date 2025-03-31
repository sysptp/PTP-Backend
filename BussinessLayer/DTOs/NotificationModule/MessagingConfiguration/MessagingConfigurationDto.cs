
/*
 * Record que se utiliza para crear una nueva configuración de WhatsApp
 * 
 * @param ConfigurationId: El id de la configuración.
 * @param BussinessId: El id de la empresa.
 * @param AccountSid: El account sid de la cuenta de WhatsApp. ACf0d0551487d0673de6da393a6eec4937
 * @param AuthToken: El auth token de la cuenta de WhatsApp.  4febec31bea02b23e8ce6e3f7554391c
 * @param WhatsAppNumber: El número desde donde se enviaran los mensajes. +12186585110
 * @param AddedBy: El usuario que agrega la configuración.
*/

namespace BussinessLayer.DTOs.NotificationModule.MessagingConfiguration{
public record MessagingConfigurationDto(
    int ConfigurationId,
    int BussinessId,
    string AccountSid,
    string AuthToken,
    string WhatsAppNumber,
    string SmsNumber,
    string AddedBy
);
}


