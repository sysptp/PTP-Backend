using BussinessLayer.DTOs.ModuloCitas.CtaEmailBackgroundJobData;
using BussinessLayer.DTOs.ModuloGeneral.Email;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Email;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BussinessLayer.Services.ModuloCitas
{
    internal class CtaBackgroundEmailService : ICtaBackgroundEmailService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<CtaBackgroundEmailService> _logger;

        public CtaBackgroundEmailService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<CtaBackgroundEmailService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public void QueueAppointmentEmails(CtaEmailBackgroundJobData emailData)
        {
            Task.Run(async () =>
            {
                // Crear un nuevo scope para cada operación de fondo
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    try
                    {
                        // Obtener los servicios necesarios dentro del scope
                        var emailService = scope.ServiceProvider.GetRequiredService<IGnEmailService>();

                        var emailTasks = new List<Task>();

                        // Enviar correos al creador
                        if (emailData.CreatorEmails != null && emailData.CreatorEmails.Any())
                        {
                            var creatorEmailMessage = new GnEmailMessageDto
                            {
                                To = emailData.CreatorEmails,
                                Subject = emailData.AssignedSubject,
                                Body = emailData.AssignedBody,
                                IsHtml = true,
                                Attachments = null,
                                CompanyId = emailData.CompanyId,
                            };
                            emailTasks.Add(emailService.SendAsync(creatorEmailMessage, emailData.CompanyId));
                        }

                        // Enviar correos a los contactos
                        if (emailData.ContactEmails != null && emailData.ContactEmails.Any())
                        {
                            var contactEmailMessage = new GnEmailMessageDto
                            {
                                To = emailData.ContactEmails,
                                Subject = emailData.ParticipantSubject,
                                Body = emailData.ParticipantBody,
                                IsHtml = true,
                                Attachments = null,
                                CompanyId = emailData.CompanyId,
                            };
                            emailTasks.Add(emailService.SendAsync(contactEmailMessage, emailData.CompanyId));
                        }

                        // Enviar correos a los usuarios del sistema
                        if (emailData.UserEmails != null && emailData.UserEmails.Any())
                        {
                            var userEmailMessage = new GnEmailMessageDto
                            {
                                To = emailData.UserEmails,
                                Subject = emailData.ParticipantSubject,
                                Body = emailData.ParticipantBody,
                                IsHtml = true,
                                Attachments = null,
                                CompanyId = emailData.CompanyId,
                            };
                            emailTasks.Add(emailService.SendAsync(userEmailMessage, emailData.CompanyId));
                        }

                        // Enviar correos a los invitados
                        if (emailData.GuestEmails != null && emailData.GuestEmails.Any())
                        {
                            var guestEmailMessage = new GnEmailMessageDto
                            {
                                To = emailData.GuestEmails,
                                Subject = emailData.ParticipantSubject,
                                Body = emailData.ParticipantBody,
                                IsHtml = true,
                                Attachments = null,
                                CompanyId = emailData.CompanyId,
                            };
                            emailTasks.Add(emailService.SendAsync(guestEmailMessage, emailData.CompanyId));
                        }

                        await Task.WhenAll(emailTasks);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error al enviar correos de cita en segundo plano: {Message}", ex.Message);
                    }
                }
            });
        }
    }
}
