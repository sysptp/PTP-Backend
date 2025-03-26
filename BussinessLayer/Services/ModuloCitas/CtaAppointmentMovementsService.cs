using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentMovements;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentMovementsService : GenericService<CtaAppointmentMovementsRequest, CtaAppointmentMovementsResponse, CtaAppointmentMovements>, ICtaAppointmentMovementsService
    {
        private readonly ICtaAppointmentMovementsRepository _repository;
        private readonly IMapper _mapper;

        public CtaAppointmentMovementsService(ICtaAppointmentMovementsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<List<CtaAppointmentMovementsResponse>> GetAllDto()
        {
            var appointmentMovementList = await _repository.GetAllWithIncludeAsync(new List<string> { "Appointments", "CtaState" });
            return _mapper.Map<List<CtaAppointmentMovementsResponse>>(appointmentMovementList);

        }
    }
}
