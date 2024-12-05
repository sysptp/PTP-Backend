using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Modulo_Citas
{
    public interface ICtaAppointmentsService : IGenericService<CtaAppointmentsRequest, CtaAppointmentsResponse,CtaAppointments>
    {
    }
}
