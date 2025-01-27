using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkSolutionTicket : AuditableEntities
    {
        [Key]
        public int IdSolution { get; set; }
        public string Descripcion { get; set; } = null!;
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public GnEmpresa? GnEmpresa { get; set; }
        public int IdDepartamento { get; set; }
        [ForeignKey("IdDepartamento")]
        public HdkDepartaments? HdkDepartaments { get; set; }
        public int OrdenStatus { get; set; }

    }
}
