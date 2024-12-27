using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.ModuloInventario.Almacen
{
    public class InvMovimientoAlmacenDetalle:AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public long IdEmpresa { get; set; }
        public int Cantidad { get; set; }        
        public bool EsVencimiento { get; set; }
        public DateTime FechaVencimiento { get; set; }
       
    }
}
