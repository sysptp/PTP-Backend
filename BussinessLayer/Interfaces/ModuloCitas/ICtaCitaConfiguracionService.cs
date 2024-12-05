using BussinessLayer.DTOs.ModuloCitas.CtaCitaConfiguracion;
using BussinessLayer.Interfaces.IOtros;

namespace DataLayer.Models.Modulo_Citas
{
    public interface ICtaCitaConfiguracionService : IGenericService<CtaCitaConfiguracionRequest, CtaCitaConfiguracionResponse, CtaCitaConfiguracion>
    {
    }
}
