using DataLayer.Models.ModuloInventario.Almacen;
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Geografia
{
    public class Municipio : AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdProvincia { get; set; }

        [ForeignKey("IdProvincia")]
        public virtual Provincia? Provincia { get; set; }

        public virtual ICollection<InvAlmacenes> Almacenes { get; set; }
    }
}
