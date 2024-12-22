using BussinessLayer.DTOs.ModuloCitas.CtaState;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaStateService : IGenericService<CtaStateRequest, CtaStateResponse, CtaState>
    {

    }
}
