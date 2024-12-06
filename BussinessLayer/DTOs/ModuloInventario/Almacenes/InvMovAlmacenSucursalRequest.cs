using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloInventario.Almacenes
{
    public class InvMovAlmacenSucursalRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdMovAlmacenSucursal { get; set; }
        public int CantidadProducto { get; set; }
        public bool Activo { get; set; }
    }
}
