using BussinessLayer.DTOs.ModuloCitas;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaAppointmentContactsService : IGenericService<CtaAppointmentContactsRequest, CtaAppointmentContactsResponse, CtaAppointmentContacts>
    {
    }
}
