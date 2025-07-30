using System.ComponentModel.DataAnnotations;
using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloGeneral.Seguridad
{
    public class GnSecurityParameters : AuditableEntities
    {
        [Key]
        public long? CompanyId { get; set; }
        public bool Requires2FA { get; set; }
        public bool AllowsOptional2FA { get; set; }
        public int PasswordExpirationDays { get; set; }
    }
}
