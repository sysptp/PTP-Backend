using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloGeneral.Seguridad
{
    public class GnPerfil : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Descripcion { get; set; }
        public long? IDEmpresa { get; set; }
    }
}
