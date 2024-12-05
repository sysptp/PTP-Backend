using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentManagement;
using BussinessLayer.Interfaces.IOtros;

namespace DataLayer.Models.Modulo_Citas
{
    public interface ICtaAppointmentManagementService : IGenericService<CtaAppointmentManagementRequest,CtaAppointmentManagementResponse,CtaAppointmentManagement>
    {
    }
}
