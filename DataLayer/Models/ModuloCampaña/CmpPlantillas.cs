using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCampaña
{
    public class CmpPlantillas : AuditableEntities
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? ContenidoHtml { get; set; }
        public string? ContenidoTexto { get; set; }
        public int EmpresaId { get; set; }
        public int TipoPlantillaId { get; set; }
        public CmpTipoPlantilla? CmpTipoPlantilla { get; set; }
    }
}
