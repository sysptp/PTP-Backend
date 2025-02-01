using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaEmailConfiguracion
{
    public class CtaEmailConfiguracionRequest
    {
        [JsonIgnore]
        public int IdEmailConfiguration { get; set; }
        public int IdUser { get; set; }
        public bool IsMailbox { get; set; } = false;
        public string Email { get; set; } = null!;
        public long CompanyId { get; set; }
    }
}
