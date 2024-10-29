using BussinessLayer.DTOs.Autenticacion;

namespace BussinessLayer.Interfaces.IAutenticacion
{
    public interface IAutenticacionService
    {
        UserSectionDto IniciarSesion(string usuario, string password);
     
    }
}
