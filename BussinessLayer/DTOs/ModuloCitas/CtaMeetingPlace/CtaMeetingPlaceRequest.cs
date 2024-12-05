using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloCitas.CtaMeetingPlace
{
    public class CtaMeetingPlaceRequest 
    {
        [JsonIgnore]
        public int IdMeetingPlace { get; set; }
        public string? Description { get; set; }
        public string? Comment { get; set; }

    }
}
