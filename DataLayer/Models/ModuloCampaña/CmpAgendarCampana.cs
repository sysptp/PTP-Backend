using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCampaña
{
    public  class CmpAgendarCampana : AuditableEntities
    {
        public int ProgramacionId { get; set; }
        public long CampanaId { get; set; }
        public CmpCampana? CmpCampana { get; set; }
        public DateTime FechaProgramada { get; set; }
        public int FrecuenciaId { get; set; }
        public CmpFrecuencia? CmpFrecuencia { get; set; }

    }
}
