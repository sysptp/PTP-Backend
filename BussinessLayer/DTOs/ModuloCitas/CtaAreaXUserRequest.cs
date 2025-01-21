
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas
{
    public class CtaAreaXUserRequest
    {
        [JsonIgnore]
        public int AreaXUserId { get; set; }
        public int UserId { get; set; }
        public int AreaId { get; set; }
        public long CompanyId { get; set; }
    }
}
