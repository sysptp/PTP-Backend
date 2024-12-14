using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkSolutionTicketReponse : AuditableEntitiesReponse
    {
        public int IdSolution { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
        public int IdDepartamento { get; set; }
        public int OrdenStatus { get; set; }
    }
}
