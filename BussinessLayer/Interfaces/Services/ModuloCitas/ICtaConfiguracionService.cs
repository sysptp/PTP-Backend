using BussinessLayer.DTOs.ModuloCitas.CtaCitaConfiguracion;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaConfiguracionService : IGenericService<CtaConfiguracionRequest, CtaConfiguracionResponse, CtaConfiguration>
    {
    }
}
