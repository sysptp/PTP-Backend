namespace BussinessLayer.DTOs.ModuloGeneral.Seguridad.GnSecurityParameters
{
    public class GnSecurityParametersResponse
    {
        public long? CompanyId { get; set; }
        public bool Requires2FA { get; set; }
        public bool AllowsOptional2FA { get; set; }
        public int PasswordExpirationDays { get; set; }
    }
}
