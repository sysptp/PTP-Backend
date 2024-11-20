using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.HelpDesk
{
    public class HdkSolutionTicket:AuditableEntities
    {
        [Key]
        public int IdSolution { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
        public int IdDepartamento { get; set; }
        public int OrdenStatus { get; set; }

    }
}
