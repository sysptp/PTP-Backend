using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataLayer.Models.ModuloGeneral.Monedas;
using DataLayer.Models.ModuloInventario.Productos;
using DataLayer.Models.ModuloGeneral.Empresa;

namespace DataLayer.Models.ModuloInventario.Otros
{
    [Table("InvProductoSuplidor")]
    public class InvProductoSuplidor
    {
        [Key]
        public int Id { get; set; }

        public int? ProductoId { get; set; }

        public int? SuplidorId { get; set; }
        public long IdEmpresa { get; set; }
        [ForeignKey("IdEmpresa")]
        public GnEmpresa? GnEmpresa { get; set; }
        public int? IdMoneda { get; set; }

        public decimal? ValorCompra { get; set; }

        public bool? Borrado { get; set; }

        public bool? Activo { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Producto? Producto { get; set; }

        [ForeignKey("SuplidorId")]
        public virtual Suplidores? Suplidor { get; set; }

        [ForeignKey("IdMoneda")]
        public virtual Moneda? Moneda { get; set; }
    }
}
