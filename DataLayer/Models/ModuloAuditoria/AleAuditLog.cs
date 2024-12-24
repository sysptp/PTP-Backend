using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloAuditoria
{
    public class AleAuditLog : AuditableEntities
    {
        [Key]
        public int AuditID { get; set; }
        public string TableName { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime AuditDate { get; set; }
        public long IdEmpresa { get; set; }

    }
}
