using BussinessLayer.DTOs.Otros;


namespace BussinessLayer.DTOs.ModuloInventario.Almacenes
{
    public class InvMovInventarioSucursalReponse: AuditableEntitiesReponse
    {
        public int Id { get; set; }
        public long IdSucursal { get; set; }
        public int IdTransaccion { get; set; }
        public int IdModulo { get; set; }
        public int IdTransaccionOrigen { get; set; }
        public int CantidadProducto { get; set; }
        public bool Activo { get; set; }
    }
}
