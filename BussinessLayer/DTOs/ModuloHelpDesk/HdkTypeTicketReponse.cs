using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkTypeTicketReponse : AuditableEntitiesReponse
    {
        public int IdTipoTicket { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
