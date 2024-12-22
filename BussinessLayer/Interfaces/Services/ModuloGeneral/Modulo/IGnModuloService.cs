using BussinessLayer.DTOs.ModuloGeneral.Modulo;
using BussinessLayer.Interfaces.Services.IOtros;
using DataLayer.Models.ModuloGeneral.Menu;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Modulo
{
    public interface IGnModuloService : IGenericService<GnModuloRequest, GnModuloResponse, GnModulo>
    {
    }
}
