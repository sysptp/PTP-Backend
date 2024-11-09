
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.Configuracion.Seguridad
{
    public class GnPerfilRequest
    {
        public string Name { get; set; } = null!;
        public string? Descripcion { get; set; }
        public long CompanyId { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
    }
}
