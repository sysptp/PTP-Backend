using BussinessLayer.DTOs.ModuloGeneral.Configuracion.Seguridad.Autenticacion;

namespace BussinessLayer.Interfaces.IAutenticacion
{
    public interface IAutenticacionService
    {
        UserSectionDto IniciarSesion(string usuario, string password);
     
    }
}
