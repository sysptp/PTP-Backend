using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkStatusTicketReponse: AuditableEntitiesReponse
    {
        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
        public bool EsCierre { get; set; }
        public long IdEmpresa { get; set; }
        public int IdDepartamento { get; set; }
        public int OrdenStatus { get; set; }
    }
}
