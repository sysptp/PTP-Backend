using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentMovements;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Modulo_Citas
{
    public interface ICtaAppointmentMovementsService : IGenericService<CtaAppointmentMovementsRequest, CtaAppointmentMovementsResponse, CtaAppointmentMovements>
    {
    }
}
