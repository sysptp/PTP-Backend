
using BussinessLayer.DTOs.ModuloCitas.CtaGuest;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaGuestService : IGenericService<CtaGuestRequest,CtaGuestResponse, CtaGuest>
    {

    }
}
