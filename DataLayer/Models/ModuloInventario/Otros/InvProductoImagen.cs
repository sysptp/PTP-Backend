using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataLayer.Models.ModuloGeneral.Imagen;
using DataLayer.Models.ModuloInventario.Productos;

namespace DataLayer.Models.ModuloInventario.Otros
{
    [Table("InvProductoImagen")]
    public class InvProductoImagen
    {
        [Key]
        public int Id { get; set; }

        public int? ProductoId { get; set; }

        public int? ImagenId { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Producto? Producto { get; set; }

        [ForeignKey("ImagenId")]
        public virtual Imagen? Imagen { get; set; }
    }
}
