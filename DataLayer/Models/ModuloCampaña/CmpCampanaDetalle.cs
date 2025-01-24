using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCampaña
{
    public class CmpCampanaDetalle : AuditableEntities
    {
        public long CampanaDetalleId { get; set; }

        public long CampanaId { get; set; }
        public CmpCampana? CmpCampana { get; set; }

        public int ClientId { get; set; }
        public CmpCliente? CmpCliente { get; set; }

        public long EmpresaId { get; set; }


    }
}
