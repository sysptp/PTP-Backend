using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloInventario.Almacenes
{
    public class InvAlmacenInventarioRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public long IdEmpresa { get; set; }
        public int IdAlmacen { get; set; }
        public int CantidadProducto { get; set; }
        public int CantidadMinima { get; set; }
        public string UbicacionExhibicion { get; set; }
        public string UbicacionGuardada { get; set; }
        public bool Activo { get; set; }
    }
}
