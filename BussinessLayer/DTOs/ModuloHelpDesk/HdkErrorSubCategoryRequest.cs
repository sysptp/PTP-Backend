
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkErrorSubCategoryRequest
    {
        [JsonIgnore]
        public int IdErroSubCategory { get; set; }
        public int IdSubCategory { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
