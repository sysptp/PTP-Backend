using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkNoteTicketReponse : AuditableEntitiesReponse
    {
        public int IdNota { get; set; }
        public string Notas { get; set; }
        public int IdTicket { get; set; }
        public long IdEmpresa { get; set; }
    }
}
