using DataLayer.Models.ModuloGeneral.Empresa;
using DataLayer.Models.ModuloGeneral.Seguridad;
using DataLayer.Models.Otros;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkDepartXUsuario : AuditableEntities
    {
        [Key]
        public int IdDepartXUsuario { get; set; }
        public string IdUsuarioDepto { get; set; } = null!;
        [ForeignKey("IdUsuarioDepto")]
        public Usuario? Usuario { get; set; }
        public int IdDepartamento { get; set; }
        [ForeignKey("IdDeparmento")]
        public HdkDepartaments? HdkDepartaments { get; set; }
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public GnEmpresa? GnEmpresa { get; set; }

    }
}
