using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaState
{
    public class CtaStateRequest 
    {
        [JsonIgnore]
        public int IdStateAppointment { get; set; }
        public string? Description { get; set; }
        public bool IsClosure { get; set; } = false;
        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
