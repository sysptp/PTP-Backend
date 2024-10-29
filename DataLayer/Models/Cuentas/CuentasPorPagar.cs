using DataLayer.Models.Almacen;
using DataLayer.Models.Otros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Cuentas
{
    public class CuentasPorPagar : BaseModel
    {
        [StringLength(250), Required]
        public string Descripcion { get; set; }

        public decimal MontoInicial { get; set; }

        [Required]
        public decimal MontoDeuda { get; set; }

        [Required]
        public decimal Restante { get; set; }

        public DateTime FechaUltimoPago { get; set; }

        public DateTime FechaLimitePago { get; set; }

        public bool IsPaid { get; set; }

        public int IdUsuario { get; set; }

        public int IdMovimientoAlmacen { get; set; }

        [ForeignKey("IdMovimientoAlmacen")]
        public MovimientoAlmacen MovimientoAlmacen { get; set; }

        [NotMapped]
        public ICollection<DetalleCuentaPorPagar> DetalleCuentasPorPagar { get; set; } = null;



    }
}
