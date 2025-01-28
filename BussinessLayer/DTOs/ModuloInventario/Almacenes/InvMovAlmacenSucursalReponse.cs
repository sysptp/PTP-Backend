using BussinessLayer.DTOs.Otros;


namespace BussinessLayer.DTOs.ModuloInventario.Almacenes
{
    public class InvMovAlmacenSucursalReponse:AuditableEntitiesReponse
    {
        public int Id { get; set; }
        public int IdAlmacen { get; set; }
        public int IdSucursal { get; set; }
        public int IdTransaccion { get; set; }
        public int IdTipoMovimiento { get; set; }
        public int CantidadProducto { get; set; }
        public string? Motivo { get; set; }
        public bool Activo { get; set; }
    }
}
