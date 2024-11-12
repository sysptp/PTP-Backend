
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.Configuracion.Geografia.DMunicipio
{
    public class MunicipioRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ProvinceId { get; set; }
    }
}
