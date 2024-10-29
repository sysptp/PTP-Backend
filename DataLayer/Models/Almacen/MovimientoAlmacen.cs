using DataLayer.Models.Otros;
using DataLayer.Models.Suplidor;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Almacen
{
    public class MovimientoAlmacen:BaseModel
    {
        public int IdSuplidor { get; set; }

        [ForeignKey("IdSuplidor")]
        public virtual Suplidores Suplidor { get; set; }

        [Required]
        public int IdTipoMovimiento { get; set; }

        [ForeignKey("IdTipoMovimiento")]
        public virtual TipoMovimiento TipoMovimiento { get; set; }

        [StringLength(30),Required]
        public string NoFactura { get; set; }

        [StringLength(30)]
        public string Ncf { get; set; }

        public int CantidadProducto { get; set; }

        public long TotalFacturado { get; set; }

        public int IdAlmacen { get; set; }

        [ForeignKey("IdAlmacen")]
        public virtual Almacenes Almacen { get; set; }

        public int IdTipoPago { get; set; }

        [ForeignKey("IdTipoPago")]
        public virtual TipoPago TipoPago { get; set; }

        public ICollection<DetalleMovimientoAlmacen> DetalleMovimientoAlmacen { get; set; }

        public string  Motivo { get; set; }
    }
}


