
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas
{
    public class CtaContactTypeRequest 
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public long CompanyId { get; set; }
    }
}
