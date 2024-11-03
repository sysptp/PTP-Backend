using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Productos
{
    public class Precio : BaseModel
    {
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Producto Producto { get; set; }

        public decimal Valor { get; set; }

        public bool Activo { get; set; }

        public int NumSeq { get; set; }
    }
}