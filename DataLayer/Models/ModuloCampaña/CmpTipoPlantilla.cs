using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCampaña
{
    public class CmpTipoPlantilla : AuditableEntities
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public long EmpresaId { get; set; }
        public ICollection<CmpPlantillas> Plantillas { get; set; }
    }
}
