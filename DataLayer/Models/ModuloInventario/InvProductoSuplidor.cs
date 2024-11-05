using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Models.ModuloInventario
{
    [Table("InvProductoSuplidor")]
    public class InvProductoSuplidor
    {
        [Key]
        public int? Id { get; set; }  // Clave primaria

        public int? ProductoId { get; set; }  // Clave externa a Producto
        public int? SuplidorId { get; set; }  // Clave externa a Suplidores
        public int? IdMoneda { get; set; }    // Clave externa a Moneda
        public decimal? ValorCompra { get; set; }  // Campo nullable para ValorCompra

        // Propiedades de navegación
        [ForeignKey("ProductoId")]
        public virtual Producto? Producto { get; set; }

        [ForeignKey("SuplidorId")]
        public virtual Suplidores? Suplidor { get; set; }

        [ForeignKey("IdMoneda")]
        public virtual Moneda? Moneda { get; set; }
    }

}
