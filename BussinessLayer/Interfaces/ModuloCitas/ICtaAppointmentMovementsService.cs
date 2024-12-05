using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentMovements;
using BussinessLayer.Interfaces.IOtros;

namespace DataLayer.Models.Modulo_Citas
{
    public interface ICtaAppointmentMovementsService : IGenericService<CtaAppointmentMovementsRequest, CtaAppointmentMovementsResponse, CtaAppointmentMovements>
    {
    }
}
