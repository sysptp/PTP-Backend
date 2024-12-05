using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interfaces.IOtros;

namespace DataLayer.Models.Modulo_Citas
{
    public interface ICtaAppointmentsService : IGenericService<CtaAppointmentsRequest, CtaAppointmentsResponse,CtaAppointments>
    {
    }
}
