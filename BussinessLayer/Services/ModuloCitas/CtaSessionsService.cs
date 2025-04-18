using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.Enums;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Interface.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Services;
using BussinessLayer.Wrappers;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaSessionsService : GenericService<CtaSessionsRequest, CtaSessionsResponse, CtaSessions>, ICtaSessionsService
    {
        private readonly ICtaSessionsRepository _sessionRepository;
        private readonly IMapper _mapper;
        private readonly ICtaAppointmentsService _appointmentsService;
        private readonly ICtaSessionDetailsRepository _sessionDetailsRepository;
        private readonly ICtaAppointmentsRepository _appointmentRepository;
        private readonly ICtaSessionEmailService _sessionEmailService;

        public CtaSessionsService(ICtaSessionsRepository sessionRepository, IMapper mapper,
            ICtaAppointmentsService appointmentsService,
            ICtaSessionDetailsRepository sessionDetailsRepository, ICtaAppointmentsRepository appointmentRepository, ICtaSessionEmailService sessionEmailService) : base(sessionRepository, mapper)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
            _appointmentsService = appointmentsService;
            _sessionDetailsRepository = sessionDetailsRepository;
            _appointmentRepository = appointmentRepository;
            _sessionEmailService = sessionEmailService;
        }
        public override async Task<List<CtaSessionsResponse>> GetAllDto()
        {
            var sessionList = await _sessionRepository.GetAllWithIncludeAsync(new List<string>
    {
        "GnRepeatUnit",
        "Usuario"
    });

            var sessionDtoList = _mapper.Map<List<CtaSessionsResponse>>(sessionList);

            foreach (var session in sessionDtoList)
            {
                session.TotalAppointments = _sessionDetailsRepository.GetAllSessionDetailsBySessionId(session.IdSession).Count();
                await MapSessionAppointmentDetailsAsync(session);
            }

            return sessionDtoList.OrderByDescending(x => x.IdSession).ToList();
        }

        public override async Task<CtaSessionsResponse> GetByIdResponse(int id)
        {
            var session = await base.GetByIdResponse(id);
            session.TotalAppointments = _sessionDetailsRepository.GetAllSessionDetailsBySessionId(session.IdSession).Count();
            var appointmentList = await _sessionDetailsRepository.GetAllAppointmentsBySessionId(session.IdSession);
            session.Participants = await _appointmentsService.GetAllParticipantsByAppointmentId(appointmentList[0].AppointmentId);

            return session;
        }

        public async Task<CtaSessionsRequest> CreateSessionAndGenerateAppointments(CtaSessionsRequest sessionRequest)
        {
            var sessionEntity = _mapper.Map<CtaSessions>(sessionRequest);
            sessionEntity = await _sessionRepository.Add(sessionEntity);

            if (sessionRequest.FirstSessionDate != default && sessionRequest.RepeatEvery > 0)
            {
                var appointments = GenerateAppointmentsForSession(sessionRequest);
                var createdAppointments = new List<CtaAppointmentsResponse>();

                foreach (var appointment in appointments)
                {
                    var appointmentEntity = await _appointmentsService.AddAppointment(appointment, true);
                    createdAppointments.Add(appointmentEntity);

                    var sessionDetail = new CtaSessionDetails
                    {
                        AppointmentId = appointmentEntity.AppointmentId,
                        IdSession = sessionEntity.IdSession,
                        IsActive = true,
                    };

                    await _sessionDetailsRepository.Add(sessionDetail);

                }

                await _sessionEmailService.SendSessionEmailsAsync(sessionRequest, createdAppointments);
            }

            return _mapper.Map<CtaSessionsRequest>(sessionEntity);
        }

        private IEnumerable<CtaAppointmentsRequest> GenerateAppointmentsForSession(CtaSessionsRequest sessionRequest)
        {
            var appointments = new List<CtaAppointmentsRequest>();
            var currentAppointmentDate = sessionRequest.FirstSessionDate;

            while (currentAppointmentDate <= sessionRequest.SessionEndDate)
            {
                var appointment = _mapper.Map<CtaAppointmentsRequest>(sessionRequest.AppointmentInformation);
                appointment.AppointmentDate = currentAppointmentDate;
                appointment.AssignedUser = sessionRequest.AssignedUser;
                appointments.Add(appointment);

                currentAppointmentDate = CalculateNextAppointmentDate(currentAppointmentDate, sessionRequest.RepeatEvery, sessionRequest.RepeatUnitId);
            }

            return appointments;
        }

        private DateTime CalculateNextAppointmentDate(DateTime currentDate, int repeatEvery, int repeatUnitId)
        {
            switch (repeatUnitId)
            {
                case (int)RepeatUnitEnum.Dia:
                    return currentDate.AddDays(repeatEvery);
                case (int)RepeatUnitEnum.Semana:
                    return currentDate.AddDays(repeatEvery * 7);
                case (int)RepeatUnitEnum.Mes:
                    return currentDate.AddMonths(repeatEvery);
                case (int)RepeatUnitEnum.Año:
                    return currentDate.AddYears(repeatEvery);
                default:
                    throw new ArgumentException("Unidad de repetición no válida");
            }
        }

        public async Task DeleteAppointmentsInSessionRange(CtaSessionsRequest sessionDto)
        {
            var existingAppointments = await _appointmentRepository.GetAppointmentsInRange(sessionDto.FirstSessionDate, sessionDto.SessionEndDate, sessionDto.AppointmentInformation.CompanyId, sessionDto.AssignedUser);

            foreach (var appointment in existingAppointments)
            {
                await _appointmentRepository.Delete(appointment.AppointmentId);
            }
        }

        public async Task<DetailMessage> GetConflictingAppointmentsInSessionRange(CtaSessionsRequest sessionDto)
        {
            var existingAppointments = await _appointmentRepository.GetAppointmentsInRange(sessionDto.FirstSessionDate, sessionDto.SessionEndDate, sessionDto.AppointmentInformation.CompanyId, sessionDto.AssignedUser);

            var conflictingAppointments = existingAppointments.Where(a =>
             (sessionDto.FirstSessionDate.Date.Add(a.AppointmentTime) >= sessionDto.FirstSessionDate &&
              sessionDto.FirstSessionDate.Date.Add(a.AppointmentTime) < sessionDto.SessionEndDate) ||

             (sessionDto.FirstSessionDate.Date.Add(a.EndAppointmentTime) > sessionDto.FirstSessionDate &&
              sessionDto.FirstSessionDate.Date.Add(a.EndAppointmentTime) <= sessionDto.SessionEndDate) ||

             (sessionDto.FirstSessionDate.Date.Add(a.AppointmentTime) <= sessionDto.FirstSessionDate &&
              sessionDto.FirstSessionDate.Date.Add(a.EndAppointmentTime) >= sessionDto.SessionEndDate)
         ).ToList();


            if (conflictingAppointments.Any())
            {
                return new DetailMessage()
                {
                    Message = "Existen citas en conflicto dentro del rango de la sesión.",
                    Details = string.Join("; ", conflictingAppointments.Select(a => $"Cita ID {a.AppointmentId} de {a.AppointmentTime} a {a.EndAppointmentTime}")),
                    Action = "¿Desea eliminar estas citas y proceder con la creación?"
                };
            }

            return null;
        }

        #region Private Methods
        private async Task MapSessionAppointmentDetailsAsync(CtaSessionsResponse session)
        {
            var appointmentList = await _sessionDetailsRepository.GetAllAppointmentsBySessionId(session.IdSession);

            if (appointmentList.Count > 0)
            {
                var firstAppointment = appointmentList[0];
                session.Participants = await _appointmentsService.GetAllParticipantsByAppointmentId(firstAppointment.AppointmentId);
                session.AppointmentDescription = firstAppointment.Description;
                session.IdReasonAppointment = firstAppointment.IdReasonAppointment;
                session.AppointmentTime = firstAppointment.AppointmentTime;
                session.IdPlaceAppointment = firstAppointment.IdPlaceAppointment;
                session.IdState = firstAppointment.IdState;
                session.IsConditionedTime = firstAppointment.IsConditionedTime;
                session.EndAppointmentTime = firstAppointment.EndAppointmentTime;
                session.AreaId = firstAppointment.AreaId;
            }
        }

        #endregion 
    }
}
