using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentReason;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentReasonService : GenericService<CtaAppointmentReasonRequest, CtaAppointmentReasonResponse, CtaAppointmentReason>, ICtaAppointmentReasonService
    {
        public CtaAppointmentReasonService(IGenericRepository<CtaAppointmentReason> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
