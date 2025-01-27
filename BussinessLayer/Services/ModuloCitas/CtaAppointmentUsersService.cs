using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaAppointmentUsersService : GenericService<CtaAppointmentUsersRequest, CtaAppointmentUsersResponse, CtaAppointmentUsers>, ICtaAppointmentUsersService
    {
        public CtaAppointmentUsersService(IGenericRepository<CtaAppointmentUsers> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
