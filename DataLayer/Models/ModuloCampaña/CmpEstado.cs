using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCampaña
{
    public class CmpEstado : AuditableEntities
    {
        public int EstadoId { get; set; }
        public string? Descripcion { get; set; }

        public ICollection<CmpCampana> Campanas { get; set; } = new List<CmpCampana>();
    }
}
