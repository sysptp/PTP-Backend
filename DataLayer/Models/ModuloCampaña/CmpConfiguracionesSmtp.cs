using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCampaña
{
    public class CmpConfiguracionesSmtp : AuditableEntities
    {
        public int ConfiguracionId {  get; set; }
        public string? Usuario { get; set; }
        public string? Contraseña { get; set; }
        public int ServidorId { get; set; }
        public long EmpresaId { get; set; }
        public CmpServidoresSmtp ServidoresSmtp { get; set; }


    }
}
