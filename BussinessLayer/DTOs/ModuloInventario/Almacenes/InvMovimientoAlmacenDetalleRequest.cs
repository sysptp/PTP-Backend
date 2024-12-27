using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloInventario.Almacenes
{
    public class InvMovimientoAlmacenDetalleRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public long IdEmpresa { get; set; }
        public int Cantidad { get; set; }
        public bool EsVencimiento { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
