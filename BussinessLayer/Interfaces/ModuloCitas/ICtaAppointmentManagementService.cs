using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentManagement;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Modulo_Citas
{
    public interface ICtaAppointmentManagementService : IGenericService<CtaAppointmentManagementRequest,CtaAppointmentManagementResponse,CtaAppointmentManagement>
    {
        Task<List<CtaAppointmentManagementResponse>> GetAllWithFilter(int? id, int? appointmentId);
    }
}
