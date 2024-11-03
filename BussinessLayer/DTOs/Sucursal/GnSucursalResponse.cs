

namespace BussinessLayer.DTOs.Sucursal
{
    public class GnSucursalResponse
    {
        public long SucursalId { get; set; }
        public long CompanyId { get; set; }
        public string SucursalName { get; set; } = null!;
        public string? Phone { get; set; }
        public int? ResponsibleUserId { get; set; }
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public int ProvinceId { get; set; }
        public int MunicipalityId { get; set; }
        public string? Address { get; set; }
        public bool SucursalStatus { get; set; }
        public string? AdditionIp { get; set; }
        public string? ModificationIp { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public bool IsPrincipal { get; set; }
    }


}
