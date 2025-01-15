using BussinessLayer.DTOs.ModuloCitas.CtaAppointments;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interfaces.Repository.ModuloCitas
{
    public interface ICtaAppointmentsRepository : IGenericService<CtaAppointmentsRequest, CtaAppointmentsResponse, CtaAppointments>
    {
    }
}
