using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaUnwanted
{ 
    public class CtaUnwantedRequest
    {
        [JsonIgnore]
        public int IdUnwanted { get; set; }
        public string EmailNumber { get; set; } = null!;
        public bool Email { get; set; } = false;
        public bool Sms { get; set; } = false;
        public bool Whatsapp { get; set; } = false;
    }
}
