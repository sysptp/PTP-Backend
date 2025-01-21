using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaSessions;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Interface.Repository.ModuloCitas;
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

        public async Task<CtaSessionsRequest> CreateSessionAndGenerateAppointments(CtaSessionsRequest sessionRequest)
        {
            var sessionEntity = _mapper.Map<CtaSessions>(sessionRequest);

            sessionEntity = await _sessionRepository.Add(sessionEntity);

            if (sessionRequest.FirstSessionDate != default && sessionRequest.FrequencyInDays > 0)
            {
                var appointments = GenerateAppointmentsForSession(sessionRequest);

                foreach (var appointment in appointments)
                {
                    await _appointmentsRepository.Add(appointment);
                }

                foreach (var appointment in appointments)
                {
                    var sessionDetail = new CtaSessionDetails
                    {
                        AppointmentId = appointment.AppointmentId,
                        IdSession = sessionEntity.IdSession,
                        IsActive = true
                    };

                    await _sessionDetailsRepository.Add(sessionDetail);
                }

            }

            return _mapper.Map<CtaSessionsRequest>(sessionEntity);
        }

        private IEnumerable<CtaAppointments> GenerateAppointmentsForSession(CtaSessionsRequest sessionRequest)
        {
            var appointments = new List<CtaAppointments>();
            var currentAppointmentDate = sessionRequest.FirstSessionDate;

            while (currentAppointmentDate <= sessionRequest.AppointmentInformation.EndAppointmentDate)
            {
                var appointment = _mapper.Map<CtaAppointments>(sessionRequest.AppointmentInformation);
                appointments.Add(appointment);

                currentAppointmentDate = currentAppointmentDate.AddDays(sessionRequest.FrequencyInDays);
            }

            return appointments;
        }
    }
}
