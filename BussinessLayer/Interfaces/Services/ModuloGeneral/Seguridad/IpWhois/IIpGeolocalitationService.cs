using BussinessLayer.DTOs.ModuloGeneral.Seguridad.IpWhois.Geolocalitation;

namespace BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad.IpWhois
{
    public interface IIpGeolocalitationService
    {
        public Task<Coordinates> GetCoordinatesAsync(string ipAddress);
    }
}
