using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaAppointmentsService : IGenericService<CtaAppointmentsRequest, CtaAppointmentsResponse, CtaAppointments>
    {
    }
}
