using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentMovements;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaAppointmentMovementsService : IGenericService<CtaAppointmentMovementsRequest, CtaAppointmentMovementsResponse, CtaAppointmentMovements>
    {
    }
}
