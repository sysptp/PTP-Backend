using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaSessions
{
    public class CtaSessionsRequest 
    {
        [JsonIgnore]
        public int IdSession { get; set; }
        public string? Description { get; set; }
        public int? IdClient { get; set; }
        public int? IdUser { get; set; }
        public DateTime FirstSessionDate { get; set; }
        public int? IdReason { get; set; }
        public int TotalAppointments { get; set; }
        public DateTime SessionEndDate { get; set; }
        [JsonIgnore]
        public int CompletedAppointments { get; set; } = 0;
        public int FrequencyInDays { get; set; }

    }
}
