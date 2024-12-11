using BussinessLayer.DTOs.ModuloCitas.CtaCitaConfiguracion;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Modulo_Citas
{
    public interface ICtaCitaConfiguracionService : IGenericService<CtaCitaConfiguracionRequest, CtaCitaConfiguracionResponse, CtaCitaConfiguracion>
    {
    }
}
