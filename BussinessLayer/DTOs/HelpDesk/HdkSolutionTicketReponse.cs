using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkSolutionTicketReponse: AuditableEntitiesReponse
    {
        public int IdSolution { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
