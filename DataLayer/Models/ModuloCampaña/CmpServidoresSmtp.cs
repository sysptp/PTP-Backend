using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCampaña
{
    public class CmpServidoresSmtp : AuditableEntities
    {
        public int ServidorId { get; set; }
        public string? Nombre { get; set; }
        public string? Host {  get; set; }
        public int Puerto { get; set; }
    }
}
