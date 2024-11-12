using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloGeneral.Sucursal
{
    public class GnSucursalRequest
    {
        public long CompanyId { get; set; }
        public string SucursalName { get; set; } = null!;
        public string? Phone { get; set; }
        public int ResponsibleUserId { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public int ProvinceId { get; set; }
        public int MunicipalityId { get; set; }
        public string? Address { get; set; }
        public bool SucursalStatus { get; set; }
        public string? UserIp { get; set; }
        public bool IsPrincipal { get; set; }
        [JsonIgnore]
        public long SucursalId { get; set; }

    }

}
