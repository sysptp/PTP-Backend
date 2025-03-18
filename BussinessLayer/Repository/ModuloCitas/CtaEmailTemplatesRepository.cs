using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Interfaces.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repository.ModuloGeneral.Seguridad;
using BussinessLayer.Repository.ROtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.PDbContex;
using Microsoft.Extensions.Configuration;

namespace BussinessLayer.Repository.ModuloCitas
{
    public class CtaEmailTemplatesRepository : GenericRepository<CtaEmailTemplates>, ICtaEmailTemplatesRepository
    {
        private readonly ICtaAppointmentAreaRepository _areaRepository;
        private readonly IUsuarioRepository _userRepository;
        private readonly ICtaAppointmentReasonRepository _appointmentReasonRepository;
        private readonly ICtaMeetingPlaceRepository _meetingPlaceRepository;
        private readonly IConfiguration _configuration;

        public CtaEmailTemplatesRepository(ICtaAppointmentAreaRepository areaRepository, IUsuarioRepository userRepository,
           ICtaAppointmentReasonRepository appointmentReasonRepository, ICtaMeetingPlaceRepository meetingPlaceRepository,
           PDbContext dbContext, ITokenService tokenService, IConfiguration configuration) : base(dbContext, tokenService)
        {
            _areaRepository = areaRepository;
            _userRepository = userRepository;
            _appointmentReasonRepository = appointmentReasonRepository;
            _meetingPlaceRepository = meetingPlaceRepository;
            _configuration = configuration;
        }

        public string ReplaceEmailTemplateValues(string templateBody, CtaAppointmentsRequest appointment, string recipientName = null)
        {
            if (string.IsNullOrEmpty(templateBody))
                return templateBody;

            var assignedUser = _userRepository.GetById(appointment.UserId).Result;
            var meetingPlace = _meetingPlaceRepository.GetById(appointment.IdPlaceAppointment).Result;
            var reason = _appointmentReasonRepository.GetById(appointment.IdReasonAppointment).Result;
            var area = _areaRepository.GetById(appointment.AreaId).Result;

            var appointmentDate = appointment.AppointmentDate.ToString("dd/MM/yyyy");
            var appointmentTime = appointment.AppointmentDate.ToString("HH:mm");
            var endAppointmentTime = appointment.EndAppointmentTime.ToString(@"hh\:mm");
            var appointmentLink = $"{_configuration["ApplicationUrl"]}/appointments/details/{appointment.AppointmentId}";

            return templateBody
                .Replace("{AssignedUser}", assignedUser?.Nombre + " "+ assignedUser?.Apellido ?? "Usuario asignado")
                .Replace("{AppointmentCode}", appointment.AppointmentCode)
                .Replace("{Description}", appointment.Description)
                .Replace("{AppointmentDate}", appointmentDate)
                .Replace("{AppointmentTime}", appointmentTime)
                .Replace("{EndAppointmentTime}", endAppointmentTime)
                .Replace("{MeetingPlaceDescription}", meetingPlace?.Description ?? "No especificado")
                .Replace("{ReasonDescription}", reason?.Description ?? "No especificado")
                .Replace("{ParticipantName}", recipientName ?? "Participante")
                .Replace("{Area}", area.Description ?? "No especificado")
                .Replace("{AppointmentLink}", appointmentLink);
        }
    }
}
