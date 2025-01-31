using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.Enums;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaSessionsService : GenericService<CtaSessionsRequest, CtaSessionsResponse, CtaSessions>, ICtaSessionsService
    {
        private readonly ICtaSessionsRepository _sessionRepository;
        private readonly IMapper _mapper;
        private readonly ICtaAppointmentsService _appointmentsService;
        private readonly ICtaSessionDetailsRepository _sessionDetailsRepository;

        public CtaSessionsService(ICtaSessionsRepository sessionRepository, IMapper mapper,
            ICtaAppointmentsService appointmentsService,
            ICtaSessionDetailsRepository sessionDetailsRepository) : base(sessionRepository,mapper)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
            _appointmentsService = appointmentsService;
            _sessionDetailsRepository = sessionDetailsRepository;
        }

        public async Task<CtaSessionsRequest> CreateSessionAndGenerateAppointments(CtaSessionsRequest sessionRequest)
        {
            var sessionEntity = _mapper.Map<CtaSessions>(sessionRequest);

            sessionEntity = await _sessionRepository.Add(sessionEntity);

            if (sessionRequest.FirstSessionDate != default && sessionRequest.RepeatEvery > 0)
            {
                var appointments = GenerateAppointmentsForSession(sessionRequest);

                foreach (var appointment in appointments)
                {
                    var appointmentEntity = await _appointmentsService.Add(appointment);
                    var sessionDetail = new CtaSessionDetails
                    {
                        AppointmentId = appointmentEntity.AppointmentId,  
                        IdSession = sessionEntity.IdSession,
                        IsActive = true
                    };

                    await _sessionDetailsRepository.Add(sessionDetail);
                }

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
    }
}
