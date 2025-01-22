using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCampaña
{
    public class CmpPlantillas : AuditableEntities
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Contenido { get; set; }
        public long EmpresaId { get; set; }
        public int TipoPlantillaId { get; set; }
        public bool EsHtml { get; set; }
        public CmpTipoPlantilla? CmpTipoPlantilla { get; set; }
        public ICollection<CmpCampana> Campanas { get; set; } = new List<CmpCampana>();

    }
}
