using System.Text.Json.Serialization;

namespace DataLayer.Models.Modulo_Citas
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
