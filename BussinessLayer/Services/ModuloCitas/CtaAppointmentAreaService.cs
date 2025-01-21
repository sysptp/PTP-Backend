using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Interfaces.Services.ModuloCitas;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Services.ModuloCitas
{
    public class CtaAppointmentAreaService : GenericService<CtaAppointmentAreaRequest, CtaAppointmentAreaResponse, CtaAppointmentArea>, ICtaAppointmentAreaService
    {
        public CtaAppointmentAreaService(IGenericRepository<CtaAppointmentArea> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
