using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloGeneral.Utils.GnMessageType
{
    public class GnMessageTypeRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Description { get; set; } = null!;
    }
}
