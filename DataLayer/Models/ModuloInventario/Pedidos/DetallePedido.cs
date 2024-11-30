using DataLayer.Models.ModuloInventario.Productos;
using DataLayer.Models.Otros;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloInventario.Pedidos
{
    [Table("InvDetallePedido")]
    public class DetallePedido 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdProducto { get; set; }

        public long? IdEmpresa { get; set; }

        public int? IdPedido { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public bool Borrado { get; set; }

        public bool? Activo { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [MaxLength(50)]
        public string? UsuarioModificacion { get; set; }

        [MaxLength(50)]
        public string? UsuarioCreacion { get; set; }

        // Navigation properties
        [ForeignKey("IdProducto")]
        public virtual Producto? Producto { get; set; }

        [ForeignKey("IdPedido")]
        public virtual Pedido? Pedido { get; set; }

    }
}