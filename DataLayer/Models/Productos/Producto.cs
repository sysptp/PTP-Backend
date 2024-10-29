using DataLayer.Models.Otros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Productos
{
    public class Producto : BaseModel
    {
        public Producto()
        {
            Descuentos = new HashSet<Descuentos>();
            FechaModificacion = DateTime.Now;
            Activo = true;
        }

        [StringLength(250)]
        public string Codigo { get; set; }

        [Required]
        public int IdVersion { get; set; }

        [ForeignKey("IdVersion")]
        public virtual Versiones Version { get; set; }

        [Required]
        public int IdEnvase { get; set; }

        [ForeignKey("IdEnvase")]
        public virtual Envase Envase { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaModificacion { get; set; }

        public bool EsLote { get; set; }

        public int CantidadLote { get; set; }

        public virtual ICollection<Imagen> Imagenes { get; set; }

        public string Imagen { get; set; }

        public bool AdmiteDescuento { get; set; } = true;

        public bool HabilitaVenta { get; set; } = true;

        public string EsProducto { get; set; }

        public int CantidadInventario { get; set; }

        public string AplicaImp { get; set; }

        public decimal ValorImpuesto { get; set; }

        public virtual ICollection<Descuentos> Descuentos { get; set; }

        [Required]
        public decimal PrecioBase { get; set; }

        [Required]
        public decimal PrecioCompra { get; set; }

        public int CantidadMinima { get; set; }

        public virtual IList<Precio> Precios { get; set; }

        [StringLength(50),Required]
        public string CodigoBarra { get; set; }
    }
}
