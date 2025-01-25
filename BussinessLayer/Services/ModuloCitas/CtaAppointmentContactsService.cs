using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaAppointmentContactsService : GenericService<CtaAppointmentContactsRequest, CtaAppointmentContactsResponse, CtaAppointmentContacts>, ICtaAppointmentContactsService
    {
        public CtaAppointmentContactsService(IGenericRepository<CtaAppointmentContacts> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
