using DataLayer.Models.Empresa;
using DataLayer.Models.Entities;
using DataLayer.Models.MenuApp;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Seguridad
{
    public class GnPermiso : AuditableEntities
    {
        [Key]
        public long IDPermiso { get; set; }
        public int IDPerfil { get; set; }
        public int IDMenu { get; set; }
        public long Codigo_EMP { get; set; }
        public bool Crear { get; set; }
        public bool Eliminar { get; set; }
        public bool Editar { get; set; }
        public bool Consultar { get; set; }
        [ForeignKey("Codigo_EMP")]
        public virtual GnEmpresa? GnEmpresa { get; set; }
        [ForeignKey("IDPerfil")]
        public virtual GnPerfil? GnPerfil { get; set; }
        [ForeignKey("IDMenu")]
        public virtual GnMenu? GnMenu { get; set; }
    }
}
