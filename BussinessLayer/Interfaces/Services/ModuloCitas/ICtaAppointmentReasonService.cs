using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentReason;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaAppointmentReasonService : IGenericService<CtaAppointmentReasonRequest, CtaAppointmentReasonResponse, CtaAppointmentReason>
    {
    }
}
