using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloInventario.Almacenes
{
    public class InvMovInventarioSucursalRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int IdSucursal { get; set; }
        public int IdTransaccion { get; set; }
        public int IdModulo { get; set; }
        public int IdTransaccionOrigen { get; set; }
        public int CantidadProducto { get; set; }
        public bool Activo { get; set; }
    }
}
