
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.Configuracion.Menu
{
    public class SaveGnMenuRequest
    {
        public string Name { get; set; } = null!;
        public int Level { get; set; }
        public int Order { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public int ModuleID { get; set; }
        public int ParentMenuId { get; set; }
        public bool Query { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        [JsonIgnore]
        public int IDMenu { get; set; }
    }
}
