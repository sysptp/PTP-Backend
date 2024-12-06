using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloGeneral.Geografia
{
    public class Region : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public int IdPais { get; set; }
        [ForeignKey("IdPais")]
        public virtual Pais? Pais { get; set; }

    }
}
