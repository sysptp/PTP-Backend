using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCampaña
{
    public class CmpCampana : AuditableEntities
    {
        public int CampanaId { get; set; }
        public string? NombreCampana { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public int EstadoId { get; set; }
        public CmpEstado? Estado { get; set; }
        public int PlantillaId { get; set; }
        public CmpPlantillas? Plantilla { get; set; }
        public int EmpresaId { get; set; }

    }
}
