using BussinessLayer.DTOs.ModuloGeneral.Modulo;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.ModuloGeneral.Menu;

namespace BussinessLayer.Interfaces.ModuloGeneral.Modulo
{
    public interface IGnModuloService : IGenericService<GnModuloRequest, GnModuloResponse, GnModulo>
    {
    }
}
