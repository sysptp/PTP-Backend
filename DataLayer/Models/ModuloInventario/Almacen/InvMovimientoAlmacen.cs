using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.ModuloInventario.Almacen
{
    public class InvMovimientoAlmacen:AuditableEntities
    {
        [Key]
        public int Id { get; set; }
        public int IdSuplidor { get; set; }
        public int IdTipoMovimiento { get; set; }
        public long IdEmpresa { get; set; }
        public int IdAlmacen { get; set; }
        public int IdTransaccion { get; set; }
        public int IdMetodoPago { get; set; }
        public string NoFactura { get; set; }
        public string Comprobante { get; set; }
        public int CantidadProducto { get; set; }
        public bool EsCredito { get; set; }
        public decimal MontoInicial { get; set; }
        public decimal MontoPendiente { get; set; }
        public long TotalFacturado { get; set; }
        public string Comentario { get; set; }
        public string NoReferencia { get; set; }
        public string NoAutorizacion { get; set; }
        
    }
}
