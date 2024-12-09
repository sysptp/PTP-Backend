using BussinessLayer.DTOs.ModuloCitas.CtaEmailConfiguracion;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.Modulo_Citas;

namespace BussinessLayer.Interface.Modulo_Citas
{
    public interface ICtaEmailConfiguracionService : IGenericService<CtaEmailConfiguracionRequest, CtaEmailConfiguracionResponse, CtaEmailConfiguracion>
    {
    }
}
