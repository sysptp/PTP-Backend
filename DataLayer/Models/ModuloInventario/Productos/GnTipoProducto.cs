using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataLayer.Models.ModuloInventario.Productos
{
    [Table("GnTipoProducto")]
    public class GnTipoProducto
    {
        [Key]
        public int? Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? NombreTipoProducto { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool? Borrado { get; set; }
        public bool? Activo { get; set; }

        // Navigation property for related Productos
        public virtual ICollection<Producto>? Productos { get; set; }
    }
}
