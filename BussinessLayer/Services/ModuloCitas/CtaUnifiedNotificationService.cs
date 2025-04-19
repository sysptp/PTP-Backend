using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.DTOs.ModuloGeneral.Email;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Email;
using DataLayer.Models.Modulo_Citas;
using DataLayer.Models.ModuloCitas;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BussinessLayer.Enums;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaUnifiedNotificationService : ICtaUnifiedNotificationService
    {
        private readonly ICtaNotificationTemplatesRepository _notificationTemplateRepo;
        private readonly ICtaStateRepository _stateRepository;
        private readonly ICtaConfiguracionRepository _configRepository;
        private readonly IGnEmailService _emailService;
        private readonly ITwilioService _messageService;
        private readonly ILogger<CtaUnifiedNotificationService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;

        public CtaUnifiedNotificationService(ICtaNotificationTemplatesRepository notificationTemplateRepo, 
            ICtaStateRepository stateRepository, 
            ICtaConfiguracionRepository configRepository,
            IGnEmailService emailService, ITwilioService messageService, 
            ILogger<CtaUnifiedNotificationService> logger, IServiceScopeFactory serviceScopeFactory, 
            IConfiguration configuration)
        {
            _notificationTemplateRepo = notificationTemplateRepo;
            _stateRepository = stateRepository;
            _configRepository = configRepository;
            _emailService = emailService;
            _messageService = messageService;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
        }

        public async Task SendNotificationsForAppointmentAsync(CtaAppointmentsRequest appointment, NotificationType notificationType, NotificationContext context)
        {
            // 1. Obtener el estado y sus plantillas de notificación
            var state = await _stateRepository.GetAllWithIncludeByIdAsync(appointment.IdState,
                new List<string> { "AssignedUserNotificationTemplate", "ParticipantNotificationTemplate", "StateChangeNotificationTemplate",
                    "AssignedUserNotificationTemplate.EmailTemplate", "AssignedUserNotificationTemplate.SmsTemplate", "AssignedUserNotificationTemplate.WhatsAppTemplate" });

            // 2. Obtener configuración para saber qué notificaciones están activas
            var config = _configRepository.GetByCompanyId(appointment.CompanyId);

            // 3. Determinar qué plantilla usar basado en el tipo de notificación
            CtaNotificationTemplates? notificationTemplate = notificationType switch
            {
                NotificationType.StateChange => state.StateChangeNotificationTemplate,
                NotificationType.ParticipantNotification => state.ParticipantNotificationTemplate,
                _ => state.AssignedUserNotificationTemplate
            };

            if (notificationTemplate == null)
            {
                _logger.LogWarning("No notification template found for type {NotificationType}", notificationType);
                return;
            }

            // 4. Enviar notificaciones basado en la configuración
            await ProcessNotificationsAsync(appointment, notificationTemplate, config, context);
        }

        private async Task ProcessNotificationsAsync(CtaAppointmentsRequest appointment, CtaNotificationTemplates notificationTemplate, CtaConfiguration config, NotificationContext context)
        {
            var tasks = new List<Task>();

            if (config.SendEmail && notificationTemplate.EmailTemplateId.HasValue)
            {
                tasks.Add(SendEmailNotificationsAsync(appointment, notificationTemplate.EmailTemplate!, context));
            }

            if (config.SendSms && notificationTemplate.SmsTemplateId.HasValue)
            {
                tasks.Add(SendSmsNotificationsAsync(appointment, notificationTemplate.SmsTemplate!, context));
            }

            if (config.SendWhatsapp && notificationTemplate.WhatsAppTemplateId.HasValue)
            {
                tasks.Add(SendWhatsAppNotificationsAsync(appointment, notificationTemplate.WhatsAppTemplate!, context));
            }

            await Task.WhenAll(tasks);
        }

        private async Task SendEmailNotificationsAsync(CtaAppointmentsRequest appointment, CtaEmailTemplates template, NotificationContext context)
        {
            Task.Run(async () =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                try
                {
                    var emailService = scope.ServiceProvider.GetRequiredService<IGnEmailService>();

                    var body = ReplaceTemplateValues(template.Body, appointment, context);
                    var subject = ReplaceTemplateValues(template.Subject, appointment, context);

                    var emailMessage = new GnEmailMessageDto
                    {
                        To = context.RecipientEmails,
                        Subject = subject,
                        Body = body,
                        IsHtml = true,
                        EmpresaId = appointment.CompanyId
                    };

                    await emailService.SendAsync(emailMessage, appointment.CompanyId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sending email notification: {Message}", ex.Message);
                }
            });
        }

        private async Task SendSmsNotificationsAsync(CtaAppointmentsRequest appointment, CtaSmsTemplates template, NotificationContext context)
        {
            Task.Run(async () =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                try
                {
                    var twilioService = scope.ServiceProvider.GetRequiredService<ITwilioService>();

                    // Obtener configuración de Twilio desde configuración de la empresa
                    var twilioConfig = GetTwilioConfig(appointment.CompanyId);

                    if (string.IsNullOrEmpty(twilioConfig.AccountSid) || string.IsNullOrEmpty(twilioConfig.AuthToken))
                    {
                        _logger.LogWarning("Twilio configuration not found for company {CompanyId}", appointment.CompanyId);
                        return;
                    }

                    var message = ReplaceTemplateValues(template.MessageContent, appointment, context);

                    foreach (var phone in context.RecipientPhoneNumbers)
                    {
                        var smsMessage = new SendMessageDto(
                            twilioConfig.AuthToken,
                            twilioConfig.AccountSid,
                            twilioConfig.FromNumber,
                            phone,
                            MessageType.SMS,
                            message,
                            (int)appointment.CompanyId
                        );

                        await twilioService.SendMessage(smsMessage);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sending SMS notification: {Message}", ex.Message);
                }
            });
        }

        private async Task SendWhatsAppNotificationsAsync(CtaAppointmentsRequest appointment, CtaWhatsAppTemplates template, NotificationContext context)
        {
            Task.Run(async () =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                try
                {
                    var twilioService = scope.ServiceProvider.GetRequiredService<ITwilioService>();

                    // Obtener configuración de Twilio desde configuración de la empresa
                    var twilioConfig = GetTwilioConfig(appointment.CompanyId);

                    if (string.IsNullOrEmpty(twilioConfig.AccountSid) || string.IsNullOrEmpty(twilioConfig.AuthToken))
                    {
                        _logger.LogWarning("Twilio configuration not found for company {CompanyId}", appointment.CompanyId);
                        return;
                    }

                    var message = ReplaceTemplateValues(template.MessageContent, appointment, context);

                    foreach (var phone in context.RecipientPhoneNumbers)
                    {
                        var whatsAppMessage = new SendMessageDto(
                            twilioConfig.AuthToken,
                            twilioConfig.AccountSid,
                            FormatWhatsAppNumber(twilioConfig.FromNumber),
                            FormatWhatsAppNumber(phone),
                            MessageType.WhatsApp,
                            message,
                            (int)appointment.CompanyId
                        );

                        await twilioService.SendMessage(whatsAppMessage);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sending WhatsApp notification: {Message}", ex.Message);
                }
            });
        }


        private string ReplaceTemplateValues(string template, CtaAppointmentsRequest appointment, NotificationContext context)
        {
            return template
                .Replace("{AssignedUser}", context.AssignedUserName)
                .Replace("{ParticipantName}", context.ParticipantName)
                .Replace("{AppointmentCode}", appointment.AppointmentCode)
                .Replace("{Description}", appointment.Description)
                .Replace("{AppointmentDate}", appointment.AppointmentDate.ToString("dd/MM/yyyy"))
                .Replace("{AppointmentTime}", appointment.AppointmentTime.ToString(@"hh\:mm"))
                .Replace("{EndAppointmentTime}", appointment.EndAppointmentTime.ToString(@"hh\:mm"))
                .Replace("{MeetingPlaceDescription}", context.MeetingPlaceDescription)
                .Replace("{ReasonDescription}", context.ReasonDescription)
                .Replace("{Area}", context.AreaDescription)
                .Replace("{PreviousState}", context.PreviousState ?? "")
                .Replace("{NewState}", context.NewState ?? "");
        }

        private string FormatWhatsAppNumber(string phoneNumber)
        {
            // Asegurarse de que el número esté en el formato correcto para WhatsApp
            // Para WhatsApp, el formato es: whatsapp:+12345678901
            if (!phoneNumber.StartsWith("whatsapp:"))
            {
                return $"whatsapp:{phoneNumber}";
            }
            return phoneNumber;
        }
    }
}
