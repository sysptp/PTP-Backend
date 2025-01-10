using BussinessLayer.DTOs.ModuloCitas.CtaState;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Modulo_Citas
{
    public interface ICtaStateService : IGenericService<CtaStateRequest, CtaStateResponse, CtaState>
    {

    }
}
