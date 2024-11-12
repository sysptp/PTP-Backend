
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkCategoryTicketRequest
    {
        [JsonIgnore]
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
