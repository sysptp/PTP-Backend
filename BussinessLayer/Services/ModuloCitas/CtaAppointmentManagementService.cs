using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentManagement;
using BussinessLayer.Interface.Repository.Modulo_Citas;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentManagementService : GenericService<CtaAppointmentManagementRequest, CtaAppointmentManagementResponse, CtaAppointmentManagement>, ICtaAppointmentManagementService
    {
        private readonly ICtaAppointmentManagementRepository _repository;
        private readonly IMapper _mapper;

        public CtaAppointmentManagementService(ICtaAppointmentManagementRepository repository, IMapper mapper) : base(repository,mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CtaAppointmentManagementResponse>> GetAllWithFilter(int? id, int? appointmentId)
        {
            var appointmentManagementList = await _repository.GetAllWithIncludeAsync(new List<string> { "Appointments"});
            var mappedAppointmentManagementList = _mapper.Map<List<CtaAppointmentManagementResponse>>(appointmentManagementList);

            if (id != null && id != 0)
            {
                appointmentManagementList.Where(x => x.IdManagementAppointment == id).ToList();
                return mappedAppointmentManagementList;
            }

            if (appointmentId != null && appointmentId != 0)
            {
                appointmentManagementList.Where(x => x.AppointmentId == appointmentId).ToList();
            }

            return mappedAppointmentManagementList;
        }
    }
}
