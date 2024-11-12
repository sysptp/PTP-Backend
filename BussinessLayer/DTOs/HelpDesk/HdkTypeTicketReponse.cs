using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkTypeTicketReponse: AuditableEntitiesReponse
    {
        public int IdTipoTicket { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
