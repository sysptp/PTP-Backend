using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataLayer.Models.ModuloGeneral;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Models.ModuloInventario
{
    [Table("InvProductoImagen")]
    public class InvProductoImagen
    {
        [Key]
        public int? Id { get; set; }  

        public int? ProductoId { get; set; }
        public int? ImagenId { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Producto? Producto { get; set; }

        [ForeignKey("ImagenId")]
        public virtual Imagen? Imagen { get; set; }
    }
}
