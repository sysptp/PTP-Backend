using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkPrioridadTicketReponse : AuditableEntitiesReponse
    {
        public int IdPrioridad { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
