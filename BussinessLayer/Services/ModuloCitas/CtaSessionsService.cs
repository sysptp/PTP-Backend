using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaSessionsService : GenericService<CtaSessionsRequest, CtaSessionsResponse, CtaSessions>, ICtaSessionsService
    {
        private readonly ICtaSessionsRepository _sessionRepository;
        private readonly IMapper _mapper;
        private readonly ICtaAppointmentsRepository _appointmentsRepository;
        private readonly ICtaSessionDetailsRepository _sessionDetailsRepository;

        public CtaSessionsService(ICtaSessionsRepository sessionRepository, IMapper mapper,
            ICtaAppointmentsRepository appointmentsRepository,
            ICtaSessionDetailsRepository sessionDetailsRepository) : base(sessionRepository,mapper)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
            _appointmentsRepository = appointmentsRepository;
            _sessionDetailsRepository = sessionDetailsRepository;
        }

        public async Task CreateSessionAndGenerateAppointments(CtaSessionsRequest sessionRequest)
        {
            var sessionEntity = _mapper.Map<CtaSessions>(sessionRequest);

            sessionEntity = await _sessionRepository.Add(sessionEntity);

            if (sessionRequest.FirstSessionDate != default && sessionRequest.FrequencyInDays > 0)
            {
                var appointments = GenerateAppointmentsForSession(
                    sessionEntity.IdSession,
                    sessionRequest.FirstSessionDate,
                    sessionRequest.SessionEndDate,
                    sessionRequest.FrequencyInDays,
                    sessionRequest.IdClient
                );

                foreach (var appointment in appointments)
                {
                    await _appointmentsRepository.Add(appointment);
                }

                foreach (var appointment in appointments)
                {
                    var sessionDetail = new CtaSessionDetails
                    {
                        IdAppointment = appointment.IdAppointment,
                        IdSession = sessionEntity.IdSession,
                        IsActive = true
                    };

                    await _sessionDetailsRepository.Add(sessionDetail);
                }
            }
        }

        private IEnumerable<CtaAppointments> GenerateAppointmentsForSession(CtaSessionsRequest sessionRequest)
        {
            var appointments = new List<CtaAppointments>();
            var currentAppointmentDate = sessionRequest.FirstSessionDate;
            const int maxAppointments = 100; 
            int appointmentCount = 0;

            while (currentAppointmentDate <= sessionRequest.EndAppointmentDate && appointmentCount < maxAppointments)
            {
                var appointment = _mapper.Map<CtaAppointments>(sessionRequest);
                appointments.Add(appointment);

                currentAppointmentDate = currentAppointmentDate.AddDays(sessionRequest.FrequencyInDays);
                appointmentCount++;
            }

            return appointments;
        }


    }
}
