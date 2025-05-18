using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Email;
using DataLayer.Models.Modulo_Citas;
using DataLayer.Models.ModuloCitas;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BussinessLayer.Enums;
using BussinessLayer.DTOs.NotificationModule.MessagingConfiguration;
using BussinessLayer.Services.NotificationModule.Contracts;
using BussinessLayer.DTOs.ModuloGeneral.Email;
using BussinessLayer.DTOs.ModuloCitas;
using Microsoft.Extensions.Configuration;
using BussinessLayer.Interfaces.RealTimeContracts;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaUnifiedNotificationService : ICtaUnifiedNotificationService
    {
        private readonly ICtaStateRepository _stateRepository;
        private readonly ICtaConfiguracionRepository _configRepository;
        private readonly ICtaEmailTemplatesRepository _emailTemplateRepository;
        private readonly ILogger<CtaUnifiedNotificationService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ICtaSmsTemplatesRepository _smsTemplateRepository;
        private readonly ICtaWhatsAppTemplatesRepository _whatsappRepository;
        private readonly ICtaNotificationTemplatesRepository _notificationTemplatesRepository;
        private readonly IConfiguration _configuration;
        private readonly INotificationService _notificationService;

        public CtaUnifiedNotificationService(
            ICtaStateRepository stateRepository,
            ICtaConfiguracionRepository configRepository,
            ICtaEmailTemplatesRepository emailTemplateRepository,
            ILogger<CtaUnifiedNotificationService> logger,
            IServiceScopeFactory serviceScopeFactory,
            ICtaSmsTemplatesRepository smsTemplateRepository,
            ICtaWhatsAppTemplatesRepository whatsappRepository,
            ICtaNotificationTemplatesRepository notificationTemplatesRepository,
            IConfiguration configuration,
            INotificationService notificationService)
        {
            _stateRepository = stateRepository;
            _configRepository = configRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _smsTemplateRepository = smsTemplateRepository;
            _whatsappRepository = whatsappRepository;
            _notificationTemplatesRepository = notificationTemplatesRepository;
            _configuration = configuration;
            this._notificationService = notificationService;
        }

        public async Task SendNotificationsForAppointmentAsync(CtaAppointmentsRequest appointment, NotificationType notificationType, NotificationContext context)
        {
            // 1. Obtener el estado
            var state = await _stateRepository.GetById(appointment.IdState);
            if (state == null)
            {
                _logger.LogWarning("State not found for appointment {AppointmentId}", appointment.AppointmentId);
                return;
            }

            // 2. Obtener configuración para saber qué notificaciones están activas
            var config = _configRepository.GetByCompanyId(appointment.CompanyId);
            if (config == null)
            {
                _logger.LogWarning("Configuration not found for company {CompanyId}", appointment.CompanyId);
                return;
            }

            // 3. Obtener plantillas de email basadas en el tipo de notificación
            long? notificationTemplateId = GetNotificationTemplateId(state, notificationType);

            // 4. Enviar notificaciones basado en la configuración
            await ProcessNotificationsAsync(appointment, notificationTemplateId, config, context);

            // Añade notificaciones en tiempo real con SignalR
            if (notificationType == NotificationType.CreationForUser || notificationType == NotificationType.CreationForParticipant)
            {
                await _notificationService.NotifyAboutAppointmentCreationAsync(
                    appointment,
                    context.AssignedUserName);
            }
            else if (notificationType == NotificationType.StateChangeForUser || notificationType == NotificationType.StateChangeForParticipant)
            {
                await _notificationService.NotifyAboutAppointmentUpdateAsync(
                    appointment,
                    context.PreviousState,
                    context.NewState);
            }
            else if (notificationType == NotificationType.UpdateForUser || notificationType == NotificationType.UpdateForParticipant)
            {
                await _notificationService.NotifyAboutAppointmentUpdateAsync(
                    appointment,
                    null,
                    null);
            }
        }

        private long? GetNotificationTemplateId(CtaState state, NotificationType notificationType)
        {
            return notificationType switch
            {
                NotificationType.StateChangeForUser => state.TemplateIdUpdate,
                NotificationType.StateChangeForParticipant => state.TemplateIdUpdateParticipant,
                NotificationType.UpdateForParticipant => state.TemplateIdUpdateParticipant,
                NotificationType.UpdateForUser => state.TemplateIdUpdate,
                NotificationType.CreationForUser => state.TemplateIdIn,
                NotificationType.CreationForParticipant => state.TemplateIdOut,
                _ => state.TemplateIdIn
            };
        }
        private async Task ProcessNotificationsAsync(CtaAppointmentsRequest appointment, long? notificationTemplateId, CtaConfiguration config, NotificationContext context)
        {
            var tasks = new List<Task>();

            // Si notificationTemplateId no existe o es cero, o el GetById falla, usamos plantillas predeterminadas
            CtaNotificationTemplates notificationTemplate = null;
            if (notificationTemplateId.HasValue && notificationTemplateId.Value > 0)
            {
                notificationTemplate = await _notificationTemplatesRepository.GetById(notificationTemplateId.Value);
            }

            // Email notifications
            if (config.SendEmail)
            {
                CtaEmailTemplates emailTemplate = null;
                if (notificationTemplate != null && notificationTemplate.EmailTemplateId.HasValue)
                {
                    emailTemplate = await _emailTemplateRepository.GetById(notificationTemplate.EmailTemplateId.Value);
                }

                // Si no hay plantilla en base de datos, usar la plantilla predeterminada del appsettings
                if (emailTemplate == null)
                {
                    // Crear una plantilla con valores predeterminados basados en el tipo de notificación
                    string templateKey = GetEmailTemplateKeyForContext(context);
                    emailTemplate = new CtaEmailTemplates
                    {
                        Subject = _configuration[$"CTAEmailTemplates:DefaultTemplates:{templateKey}:Subject"],
                        Body = _configuration[$"CTAEmailTemplates:DefaultTemplates:{templateKey}:Body"]
                    };

                    _logger.LogInformation("Using default email template from appsettings: {TemplateKey}", templateKey);
                }

                tasks.Add(SendEmailNotificationsAsync(appointment, emailTemplate, context));
            }

            // SMS notifications
            if (config.SendSms)
            {
                CtaSmsTemplates smsTemplate = null;
                if (notificationTemplate != null && notificationTemplate.SmsTemplateId.HasValue)
                {
                    smsTemplate = await _smsTemplateRepository.GetById(notificationTemplate.SmsTemplateId.Value);
                }

                // Si no hay plantilla en base de datos, usar la plantilla predeterminada del appsettings
                if (smsTemplate == null)
                {
                    string templateKey = GetSmsTemplateKeyForContext(context);
                    smsTemplate = new CtaSmsTemplates
                    {
                        MessageContent = _configuration[$"CTAMessageTemplates:SMS:{templateKey}"]
                    };

                    _logger.LogInformation("Using default SMS template from appsettings: {TemplateKey}", templateKey);
                }

                // Modificar el contenido para SMS si es un cambio de estado
                if (context.PreviousState != null && context.NewState != null)
                {
                    var originalContent = smsTemplate.MessageContent;
                    smsTemplate.MessageContent = ModifySmsTemplateForStateChange(originalContent, context.PreviousState, context.NewState);
                }

                tasks.Add(SendSmsNotificationsAsync(appointment, smsTemplate, context));
            }

            // WhatsApp notifications
            if (config.SendWhatsapp)
            {
                CtaWhatsAppTemplates whatsAppTemplate = null;
                if (notificationTemplate != null && notificationTemplate.WhatsAppTemplateId.HasValue)
                {
                    whatsAppTemplate = await _whatsappRepository.GetById(notificationTemplate.WhatsAppTemplateId.Value);
                }

                // Si no hay plantilla en base de datos, usar la plantilla predeterminada del appsettings
                if (whatsAppTemplate == null)
                {
                    string templateKey = GetWhatsAppTemplateKeyForContext(context);
                    whatsAppTemplate = new CtaWhatsAppTemplates
                    {
                        MessageContent = _configuration[$"CTAMessageTemplates:WhatsApp:{templateKey}"],
                        IsInteractive = false
                    };

                    _logger.LogInformation("Using default WhatsApp template from appsettings: {TemplateKey}", templateKey);
                }

                // Modificar el contenido para WhatsApp si es un cambio de estado
                if (context.PreviousState != null && context.NewState != null)
                {
                    var originalContent = whatsAppTemplate.MessageContent;
                    whatsAppTemplate.MessageContent = ModifyWhatsAppTemplateForStateChange(originalContent, context.PreviousState, context.NewState);
                }

                tasks.Add(SendWhatsAppNotificationsAsync(appointment, whatsAppTemplate, context));
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

                    string body = template.Body;
                    string subject = template.Subject;

                    // Si hay información de cambio de estado, modificar la plantilla
                    if (context.PreviousState != null && context.NewState != null)
                    {
                        // Modificar el asunto para reflejar el cambio de estado
                        subject = "Cambio de Estado en Cita: " + subject;

                        // Añadir sección de cambio de estado al cuerpo
                        body = ModifyEmailTemplateForStateChange(body);
                    }

                    // Aplicar las variables de sustitución
                    body = ReplaceTemplateValues(body, appointment, context);
                    subject = ReplaceTemplateValues(subject, appointment, context);

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

        // Método para modificar la plantilla de correo y añadir la sección de cambio de estado
        private string ModifyEmailTemplateForStateChange(string templateBody)
        {
            const string stateChangeSection = "<div class='state-change' style='background-color: #f8f9fa; padding: 15px; border-radius: 8px; margin: 15px 0;'><p><strong>El estado ha cambiado de:</strong> {PreviousState}</p><p><strong>A:</strong> {NewState}</p></div>";

            // Buscar un lugar adecuado para insertar la sección de cambio de estado
            if (templateBody.Contains("</ul><p>"))
            {
                // Insertar después de la lista de detalles
                return templateBody.Replace("</ul><p>", "</ul>" + stateChangeSection + "<p>");
            }
            else if (templateBody.Contains("</ul>"))
            {
                // Insertar después de cualquier lista
                return templateBody.Replace("</ul>", "</ul>" + stateChangeSection);
            }
            else if (templateBody.Contains("<div class='content'>"))
            {
                // Insertar al inicio del contenido
                return templateBody.Replace("<div class='content'>", "<div class='content'>" + stateChangeSection);
            }
            else
            {
                // Si no hay un lugar ideal, añadir antes del cierre del body
                return templateBody.Replace("</body>", stateChangeSection + "</body>");
            }
        }

        private async Task SendSmsNotificationsAsync(CtaAppointmentsRequest appointment, CtaSmsTemplates ctaSmsTemplates, NotificationContext context)
        {
            Task.Run(async () =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                try
                {
                    var messagingConfigRepo = scope.ServiceProvider.GetRequiredService<IMessagingConfigurationRepository>();
                    var messageService = scope.ServiceProvider.GetRequiredService<IMessageService>();

                    // Obtener configuración de mensajería desde configuración de la empresa
                    var messagingConfig = messagingConfigRepo.GetByCompanyIdAsync(appointment.CompanyId);
                    if (messagingConfig == null)
                    {
                        _logger.LogWarning("Messaging configuration not found for company {CompanyId}", appointment.CompanyId);
                        return;
                    }

                    var message = ReplaceTemplateValues(ctaSmsTemplates.MessageContent, appointment, context);

                    foreach (var phone in context.RecipientPhoneNumbers)
                    {
                        var smsMessage = new SendMessageDto(
                            messagingConfig.ConfigurationId,
                            phone,
                            MessageType.SMS,
                            message,
                            appointment.CompanyId
                        );

                        await messageService.SendMessage(smsMessage);
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
                    var messagingConfigRepo = scope.ServiceProvider.GetRequiredService<IMessagingConfigurationRepository>();
                    var messageService = scope.ServiceProvider.GetRequiredService<IMessageService>();

                    // Obtener configuración de mensajería desde configuración de la empresa
                    var messagingConfig = messagingConfigRepo.GetByCompanyIdAsync(appointment.CompanyId);
                    if (messagingConfig == null)
                    {
                        _logger.LogWarning("Messaging configuration not found for company {CompanyId}", appointment.CompanyId);
                        return;
                    }

                    var message = ReplaceTemplateValues(template.MessageContent, appointment, context);
                    message = message.Replace("\\n", "\n");

                    var isInteractive = template.IsInteractive;
                    var buttonConfig = template.ButtonConfig;

                    foreach (var phone in context.RecipientPhoneNumbers)
                    {
                        var whatsAppMessage = new SendMessageDto(
                            messagingConfig.ConfigurationId,
                            FormatWhatsAppNumber(phone),
                            MessageType.WhatsApp,
                            message,
                            appointment.CompanyId
                        );
                        
                        await messageService.SendMessage(whatsAppMessage);
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
                .Replace("{AssignedUser}", context.AssignedUserName ?? "")
                .Replace("{ParticipantName}", "Estimados participantes")
                .Replace("{AppointmentCode}", appointment.AppointmentCode ?? "")
                .Replace("{Description}", appointment.Description ?? "")
                .Replace("{AppointmentDate}", appointment.AppointmentDate.ToString("dd/MM/yyyy"))
                .Replace("{AppointmentTime}", appointment.AppointmentTime.ToString(@"hh\:mm"))
                .Replace("{EndAppointmentTime}", appointment.EndAppointmentTime.ToString(@"hh\:mm"))
                .Replace("{MeetingPlaceDescription}", context.MeetingPlaceDescription ?? "")
                .Replace("{ReasonDescription}", context.ReasonDescription ?? "")
                .Replace("{Area}", context.AreaDescription ?? "")
                .Replace("{PreviousState}", context.PreviousState ?? "")
                .Replace("{NewState}", context.NewState ?? "");
        }

        private string FormatWhatsAppNumber(string phoneNumber)
        {
            if (!phoneNumber.StartsWith("whatsapp:"))
            {
                return $"{phoneNumber}";
            }
            return phoneNumber;
        }

        // Método para modificar el mensaje SMS para cambio de estado
        private string ModifySmsTemplateForStateChange(string smsContent, string previousState, string newState)
        {
            // Añadir información de cambio de estado al final del mensaje SMS
            return smsContent + $"\nCambio de estado: {previousState} → {newState}";
        }

        // Método para modificar el mensaje WhatsApp para cambio de estado
        private string ModifyWhatsAppTemplateForStateChange(string whatsAppContent, string previousState, string newState)
        {
            // Añadir información de cambio de estado al final del mensaje WhatsApp
            return whatsAppContent + $"\n*Cambio de estado:* {previousState} → {newState}";
        }

        private string GetEmailTemplateKeyForContext(NotificationContext context)
        {
            if (context.PreviousState != null && context.NewState != null)
            {
                return "StateChangeTemplate";
            }

            if (context.IsUpdate)
            {
                return context.IsForAssignedUser
                    ? "UpdatedAppointmentTemplate"
                    : "UpdatedParticipantTemplate";
            }

            return context.IsForAssignedUser
                ? "AssignedUserTemplate"
                : "ParticipantTemplate";
        }

        private string GetSmsTemplateKeyForContext(NotificationContext context)
        {
            if (context.PreviousState != null && context.NewState != null)
            {
                return "StateChangeTemplate";
            }

            if (context.IsUpdate)
            {
                return "UpdatedAppointmentTemplate";
            }

            return context.IsForAssignedUser
                ? "AssignedUserTemplate"
                : "ParticipantTemplate";
        }

        private string GetWhatsAppTemplateKeyForContext(NotificationContext context)
        {
            if (context.PreviousState != null && context.NewState != null)
            {
                return "StateChangeTemplate";
            }

            if (context.IsUpdate)
            {
                return "UpdatedAppointmentTemplate";
            }

            return context.IsForAssignedUser
                ? "AssignedUserTemplate"
                : "ParticipantTemplate";
        }
    }
}