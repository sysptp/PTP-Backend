using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Entities
{
    public class GnPerfil : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public string Perfil { get; set; } = null!;
        public string? Descripcion { get; set; }
        public long? IDEmpresa { get; set; }
    }
}
