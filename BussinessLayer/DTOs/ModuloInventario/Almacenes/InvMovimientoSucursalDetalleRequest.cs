using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloInventario.Almacenes
{
    public class InvMovimientoSucursalDetalleRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdMovInventarioSucursal { get; set; }
        public int CantidadProducto { get; set; }
        public bool Activo { get; set; }
    }
}
