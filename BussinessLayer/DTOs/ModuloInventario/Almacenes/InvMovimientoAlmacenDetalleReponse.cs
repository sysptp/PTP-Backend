using BussinessLayer.DTOs.Otros;


namespace BussinessLayer.DTOs.ModuloInventario.Almacenes
{
    public class InvMovimientoAlmacenDetalleReponse: AuditableEntitiesReponse
    {
        public int Id { get; set; }
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public long IdEmpresa { get; set; }
        public int Cantidad { get; set; }
        public bool EsVencimiento { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
