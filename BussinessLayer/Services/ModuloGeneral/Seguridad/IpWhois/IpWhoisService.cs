using BussinessLayer.DTOs.ModuloGeneral.Seguridad.IpWhois.Geolocalitation;
using BussinessLayer.Interfaces.Services.ModuloGeneral.Seguridad.IpWhois;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BussinessLayer.Services.ModuloGeneral.Seguridad.IpWhois
{

    public class IpWhoisService : IIpGeolocalitationService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiUrl;

        public IpWhoisService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApisExternas:IpWhois"];
        }

        public async Task<Coordinates> GetCoordinatesAsync(string ipAddress)
        {
            var apiUrl = $"https://ipwhois.app/json/{ipAddress}";

            var response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error al obtener geolocalización.");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var geolocationData = JsonSerializer.Deserialize<IpWhoisResponse>(jsonResponse);

            if (geolocationData == null || geolocationData.Success == false)
                throw new Exception("No se pudo obtener la localización.");

            return new Coordinates { Latitude = geolocationData.Latitude, Longitude = geolocationData.Longitude };
        }

    }

}
