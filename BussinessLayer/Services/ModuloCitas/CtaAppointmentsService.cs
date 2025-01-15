using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interface.Repository.ModuloCitas;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentsService : GenericService<CtaAppointmentsRequest, CtaAppointmentsResponse, CtaAppointments>, ICtaAppointmentsRepository
    {
        public CtaAppointmentsService(IGenericRepository<CtaAppointments> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
