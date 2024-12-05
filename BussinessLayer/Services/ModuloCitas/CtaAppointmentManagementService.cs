using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentManagement;
using BussinessLayer.Interface.Modulo_Citas;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentManagementService : GenericService<CtaAppointmentManagementRequest, CtaAppointmentManagementResponse, CtaAppointmentManagement>, ICtaAppointmentManagementService
    {
        public CtaAppointmentManagementService(IGenericRepository<CtaAppointmentManagement> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
