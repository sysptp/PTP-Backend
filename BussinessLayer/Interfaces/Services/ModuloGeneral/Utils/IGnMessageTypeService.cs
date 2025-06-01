using BussinessLayer.DTOs.ModuloGeneral.Utils.GnMessageType;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloCitas;
using DataLayer.Models.ModuloGeneral.Utils;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Utils
{
    public interface IGnMessageTypeService : IGenericService<GnMessageTypeRequest, GnMessageTypeResponse, GnMessageType>
    {
    }
}
