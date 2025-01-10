using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentReason;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Modulo_Citas
{
    public interface ICtaAppointmentReasonService : IGenericService<CtaAppointmentReasonRequest, CtaAppointmentReasonResponse, CtaAppointmentReason>
    {
    }
}
