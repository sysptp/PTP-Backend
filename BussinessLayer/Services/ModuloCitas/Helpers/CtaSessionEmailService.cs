using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.DTOs.ModuloGeneral.Email;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Interface.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Email;
using DataLayer.Models.Modulo_Citas;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaSessionEmailService : ICtaSessionEmailService
    {
        private readonly IUsuarioRepository _userRepository;
        private readonly ICtaStateRepository _ctaStateRepository;
        private readonly ICtaEmailTemplatesRepository _ctaEmailTemplateRepository;
        private readonly IGnEmailService _gnEmailService;
        private readonly ICtaAppointmentsService _appointmentsService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CtaSessionEmailService(
            IUsuarioRepository userRepository,
            ICtaStateRepository ctaStateRepository,
            ICtaEmailTemplatesRepository ctaEmailTemplateRepository,
            IGnEmailService gnEmailService,
            ICtaAppointmentsService appointmentsService,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _ctaStateRepository = ctaStateRepository;
            _ctaEmailTemplateRepository = ctaEmailTemplateRepository;
            _gnEmailService = gnEmailService;
            _appointmentsService = appointmentsService;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task SendSessionEmailsAsync(CtaSessionsRequest sessionRequest, List<CtaAppointmentsResponse> appointments)
        {
            try
            {
                var creator = await _userRepository.GetById(sessionRequest.AssignedUser);
                var appointmentState = await _ctaStateRepository.GetById(sessionRequest.AppointmentInformation.IdState);

                var emailTemplates = await GetEmailTemplates(appointmentState);

                var participantEmails = await GetParticipantEmails(sessionRequest);

                var appointmentsHtml = CreateAppointmentsHtmlTable(appointments);

                var emailContent = PrepareEmailContent(
                    emailTemplates.AssignedSubject,
                    emailTemplates.AssignedBody,
                    emailTemplates.ParticipantSubject,
                    emailTemplates.ParticipantBody,
                    sessionRequest,
                    appointmentsHtml);

                await SendCreatorEmail(creator, emailContent.AssignedSubject, emailContent.AssignedBody, sessionRequest.AppointmentInformation.CompanyId);

                await SendParticipantsEmail(participantEmails, emailContent.ParticipantSubject, emailContent.ParticipantBody, sessionRequest.AppointmentInformation.CompanyId);
            }
            catch (Exception ex)
            {
            }
        }

        private async Task<(string AssignedSubject, string AssignedBody, string ParticipantSubject, string ParticipantBody)> GetEmailTemplates(CtaState appointmentState)
        {
            var configAssignedSubject = _configuration["EmailTemplates:DefaultTemplates:AssignedUserTemplate:Subject"];
            var configAssignedBody = _configuration["EmailTemplates:DefaultTemplates:AssignedUserTemplate:Body"];
            var configParticipantSubject = _configuration["EmailTemplates:DefaultTemplates:ParticipantTemplate:Subject"];
            var configParticipantBody = _configuration["EmailTemplates:DefaultTemplates:ParticipantTemplate:Body"];

            var emailTemplateForAssignedUser = await _ctaEmailTemplateRepository.GetById(appointmentState.EmailTemplateIdIn);
            var emailTemplateForParticipant = await _ctaEmailTemplateRepository.GetById(appointmentState.EmailTemplateIdOut);

            return (
                AssignedSubject: emailTemplateForAssignedUser?.Subject ?? configAssignedSubject,
                AssignedBody: emailTemplateForAssignedUser?.Body ?? configAssignedBody,
                ParticipantSubject: emailTemplateForParticipant?.Subject ?? configParticipantSubject,
                ParticipantBody: emailTemplateForParticipant?.Body ?? configParticipantBody
            );
        }

        private async Task<List<string>> GetParticipantEmails(CtaSessionsRequest sessionRequest)
        {
            var participants = await _appointmentsService.GetAllParticipants();
            var participantEmails = new List<string>();

            if (sessionRequest.AppointmentInformation.AppointmentParticipants != null)
            {
                foreach (var participant in sessionRequest.AppointmentInformation.AppointmentParticipants)
                {
                    var matchingParticipant = participants.FirstOrDefault(p =>
                        p.ParticipantId == participant.ParticipantId &&
                        p.ParticipantTypeId == participant.ParticipantTypeId);

                    if (matchingParticipant != null && !string.IsNullOrEmpty(matchingParticipant.ParticipantEmail))
                    {
                        participantEmails.Add(matchingParticipant.ParticipantEmail);
                    }
                }
            }

            return participantEmails;
        }

        private string CreateAppointmentsHtmlTable(List<CtaAppointmentsResponse> appointments)
        {
            var appointmentsHtml = new StringBuilder();
            appointmentsHtml.AppendLine("<h3>Listado de citas programadas:</h3>");
            appointmentsHtml.AppendLine("<table border='1' cellpadding='5' style='border-collapse: collapse;'>");
            appointmentsHtml.AppendLine("<tr><th>Fecha</th><th>Hora inicio</th><th>Hora fin</th><th>Descripción</th></tr>");

            foreach (var appointment in appointments.OrderBy(a => a.AppointmentDate).ThenBy(a => a.AppointmentTime))
            {
                appointmentsHtml.AppendLine("<tr>");
                appointmentsHtml.AppendLine($"<td>{appointment.AppointmentDate:dd/MM/yyyy}</td>");
                appointmentsHtml.AppendLine($"<td>{appointment.AppointmentTime:hh\\:mm}</td>");
                appointmentsHtml.AppendLine($"<td>{appointment.EndAppointmentTime:hh\\:mm}</td>");
                appointmentsHtml.AppendLine($"<td>{appointment.Description}</td>");
                appointmentsHtml.AppendLine("</tr>");
            }

            appointmentsHtml.AppendLine("</table>");
            return appointmentsHtml.ToString();
        }

        private (string AssignedSubject, string AssignedBody, string ParticipantSubject, string ParticipantBody) PrepareEmailContent(
            string assignedSubject,
            string assignedBody,
            string participantSubject,
            string participantBody,
            CtaSessionsRequest sessionRequest,
            string appointmentsHtml)
        {
            assignedSubject = assignedSubject.Replace("nueva cita", "nueva sesión de citas");
            participantSubject = participantSubject.Replace("nueva cita", "nueva sesión de citas");

            assignedBody = _ctaEmailTemplateRepository.ReplaceEmailTemplateValues(assignedBody,_mapper.Map<CtaAppointmentsRequest>(sessionRequest.AppointmentInformation));
            participantBody = _ctaEmailTemplateRepository.ReplaceEmailTemplateValues(participantBody, _mapper.Map<CtaAppointmentsRequest>(sessionRequest.AppointmentInformation));

            assignedBody = assignedBody.Replace("</body>", appointmentsHtml + "</body>");
            participantBody = participantBody.Replace("</body>", appointmentsHtml + "</body>");

            return (assignedSubject, assignedBody, participantSubject, participantBody);
        }

        private async Task SendCreatorEmail(object creator, string subject, string body, long companyId)
        {
            var creatorEmail = creator.GetType().GetProperty("Email")?.GetValue(creator)?.ToString();

            if (!string.IsNullOrEmpty(creatorEmail))
            {
                var creatorEmailMessage = new GnEmailMessageDto
                {
                    To = new List<string> { creatorEmail },
                    Subject = subject,
                    Body = body,
                    IsHtml = true,
                    Attachments = null,
                    EmpresaId = companyId
                };

                await _gnEmailService.SendAsync(creatorEmailMessage, companyId);
            }
        }

        private async Task SendParticipantsEmail(List<string> participantEmails, string subject, string body, long companyId)
        {
            if (participantEmails.Any())
            {
                var participantEmailMessage = new GnEmailMessageDto
                {
                    To = participantEmails,
                    Subject = subject,
                    Body = body,
                    IsHtml = true,
                    Attachments = null,
                    EmpresaId = companyId
                };

                await _gnEmailService.SendAsync(participantEmailMessage, companyId);
            }
        }
    }
}