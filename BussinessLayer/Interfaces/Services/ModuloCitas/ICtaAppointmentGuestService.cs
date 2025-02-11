using BussinessLayer.DTOs.ModuloCitas.CtaAppointmentGuest;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaAppointmentGuestService : IGenericService<CtaAppointmentGuestRequest, CtaAppointmentGuestResponse, CtaAppointmentGuest>
    {
    }
}
