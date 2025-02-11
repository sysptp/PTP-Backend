using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentGuest;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaAppointmentGuestService : GenericService<CtaAppointmentGuestRequest, CtaAppointmentGuestResponse, CtaAppointmentGuest>, ICtaAppointmentGuestService
    {
        public CtaAppointmentGuestService(IGenericRepository<CtaAppointmentGuest> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
