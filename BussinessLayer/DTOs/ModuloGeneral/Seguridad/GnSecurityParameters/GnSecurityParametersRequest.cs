using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloGeneral.Seguridad.GnSecurityParameters
{
    public class GnSecurityParametersRequest
    {
        public long? CompanyId { get; set; }
        public bool Requires2FA { get; set; }
        public bool AllowsOptional2FA { get; set; }
        [JsonIgnore]
        public int PasswordExpirationDays { get; set; } = 0;
    }
}
