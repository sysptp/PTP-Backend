using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkSubCategory : AuditableEntities
    {
        [Key]
        public int IdSubCategory { get; set; }
        public int IdCategory { get; set; }
        public string Descripcion { get; set; } = null!;
        public int CantidadHoraSLA { get; set; }
        public bool EsAsignacionDirecta { get; set; }
        public int IdDepartamento { get; set; }
        [ForeignKey("IdDepartamento")]
        public HdkDepartaments? HdkDepartaments { get; set; }
        public string IdUsuarioAsignacion { get; set; } = null!;
        [ForeignKey("IdUsuarioAsignacion")]
        public Usuario? Usuario { get; set; }
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public GnEmpresa? GnEmpresa { get; set; }
    }
}
