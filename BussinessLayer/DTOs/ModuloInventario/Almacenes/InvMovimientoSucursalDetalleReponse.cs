using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloInventario.Almacenes
{
    public class InvMovimientoSucursalDetalleReponse:AuditableEntitiesReponse
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdMovInventarioSucursal { get; set; }
        public int CantidadProducto { get; set; }
        public bool Activo { get; set; }
    }
}
