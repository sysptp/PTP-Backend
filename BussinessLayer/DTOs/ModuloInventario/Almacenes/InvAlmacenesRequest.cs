using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloInventario.Almacenes
{
    public class InvAlmacenesRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public long IdEmpresa { get; set; }
        public int MunicipioId { get; set; }
        public int IdUsuario { get; set; }
        public long IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool EsPrincipal { get; set; }
    }
}
