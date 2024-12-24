
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloAuditoria
{
    public class AleAuditLogRequest 
    {
        [JsonIgnore]
        public int AuditID { get; set; }
        public string TableName { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime AuditDate { get; set; }
        public long IdEmpresa { get; set; }

    }
}
