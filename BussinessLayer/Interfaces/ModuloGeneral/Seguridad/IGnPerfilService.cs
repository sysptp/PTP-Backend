using BussinessLayer.DTOs.Configuracion.Seguridad;
using BussinessLayer.Interface.IOtros;
using BussinessLayer.Interfaces.IOtros;
using BussinessLayer.Services;
using DataLayer.Models.Entities;


namespace BussinessLayer.Interfaces.ModuloGeneral.Seguridad
{
    public interface IGnPerfilService : IGenericService<GnPerfilRequest, GnPerfilResponse, GnPerfil>
    {
    }

}
