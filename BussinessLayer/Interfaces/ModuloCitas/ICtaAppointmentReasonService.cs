using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentReason;
using BussinessLayer.Interfaces.IOtros;

namespace DataLayer.Models.Modulo_Citas
{
    public interface ICtaAppointmentReasonService : IGenericService<CtaAppointmentReasonRequest, CtaAppointmentReasonResponse, CtaAppointmentReason>
    {
    }
}
