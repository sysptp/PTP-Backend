using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloGeneral.Geografia
{
    public class Provincia : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public int IdRegion { get; set; }
        [ForeignKey("IdRegion")]
        public virtual Region? Region { get; set; }
    }
}
