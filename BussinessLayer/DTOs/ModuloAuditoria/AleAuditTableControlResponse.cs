namespace BussinessLayer.DTOs.ModuloAuditoria
{
    public class AleAuditTableControlResponse
    {
        public long Id { get; set; }
        public string TableName { get; set; } = null!;
        public bool EnableInsert { get; set; }
        public bool EnableUpdate { get; set; }
        public bool EnableDelete { get; set; }
        public long IdEmpresa { get; set; }
    }
}
