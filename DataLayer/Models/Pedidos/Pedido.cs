using DataLayer.Models.ModuloInventario;
using DataLayer.Models.Otros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Pedidos
{
    public class Pedido : BaseModel
    {
        public Pedido()
        {
            FechaModificacion = DateTime.Now;
        }

        [Display(Name = "Suplidor")]
        [Required(ErrorMessage = "Suplidor Requerido")]
        public int IdSuplidor { get; set; }

        [ForeignKey("IdSuplidor")]
        public Suplidores Suplidor { get; set; }

        public bool Solicitado { get; set; }

        public int Estado { get; set; }

        public virtual IList<DetallePedido> Detalle{ get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}