using BussinessLayer.DTOs.ModuloCitas.CtaEmailConfiguracion;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interfaces.Services.ModuloCitas
{
    public interface ICtaEmailConfiguracionService : IGenericService<CtaEmailConfiguracionRequest, CtaEmailConfiguracionResponse, CtaEmailConfiguration>
    {
    }
}
