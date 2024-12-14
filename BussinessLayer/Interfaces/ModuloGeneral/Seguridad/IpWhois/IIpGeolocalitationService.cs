using BussinessLayer.DTOs.ModuloGeneral.Seguridad.IpWhois.Geolocalitation;

namespace BussinessLayer.Interfaces.ModuloGeneral.Seguridad.IpWhois
{
    public interface IIpGeolocalitationService
    {
        public Task<Coordinates> GetCoordinatesAsync(string ipAddress);
    }
}
