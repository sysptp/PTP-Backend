using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkDepartaments : AuditableEntities
    {
        [Key]
        public int IdDepartamentos { get; set; }
        public string Descripcion { get; set; } = null!;
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public GnEmpresa? GnEmpresa { get; set; }
        public bool EsPrincipal { get; set; }

    }
}
