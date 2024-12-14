using BussinessLayer.DTOs.ModuloGeneral.Seguridad.Autenticacion;

namespace BussinessLayer.Interfaces.IAutenticacion
{
    public interface IAutenticacionService
    {
        UserSectionDto IniciarSesion(string usuario, string password);
     
    }
}
