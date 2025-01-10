using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaSessions
{
    public class CtaSessionsRequest 
    {
        [JsonIgnore]
        public int IdSession { get; set; }
        public string? Description { get; set; }
        public string? IdClient { get; set; }
        public string? IdUser { get; set; }
        public DateTime FirstSessionDate { get; set; }
        public int? IdReason { get; set; }
        public int IdState { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
