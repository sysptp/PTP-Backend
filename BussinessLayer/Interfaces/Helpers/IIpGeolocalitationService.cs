using BussinessLayer.DTOs.Helper.Geolocalitation;

namespace BussinessLayer.Interfaces.Helpers
{
    public interface IIpGeolocalitationService
    {
        public Task<Coordinates> GetCoordinatesAsync(string ipAddress);
    }
}
