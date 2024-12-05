using AutoMapper;
using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentMovements;
using BussinessLayer.Interfaces.Repositories;
using BussinessLayer.Services;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaAppointmentMovementsService : GenericService<CtaAppointmentMovementsRequest, CtaAppointmentMovementsResponse, CtaAppointmentMovements>, ICtaAppointmentMovementsService
    {
        public CtaAppointmentMovementsService(IGenericRepository<CtaAppointmentMovements> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
