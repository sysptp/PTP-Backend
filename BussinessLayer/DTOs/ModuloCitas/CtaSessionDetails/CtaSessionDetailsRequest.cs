using System.Text.Json.Serialization;

namespace DataLayer.Models.Modulo_Citas
{
    public class CtaSessionDetailsRequest 
    {
        [JsonIgnore]
        public int IdSessionDetail { get; set; }
        public int IdAppointment { get; set; }
        public int IdSession { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
