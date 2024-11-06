using BussinessLayer.DTOs.Configuracion.Modulo;
using BussinessLayer.Interfaces.IOtros;
using DataLayer.Models.MenuApp;

namespace BussinessLayer.Interfaces.IModulo
{
    public interface IGnModuloService : IGenericService<GnModuloRequest,GnModuloResponse,GnModulo>
    {
    }
}
