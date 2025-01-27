using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCampaña
{
    public class CmpFrecuencia : AuditableEntities
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public ICollection<CmpAgendarCampana> CmpAgendarCampanas { get; set; } = new List<CmpAgendarCampana>();
    }
}
