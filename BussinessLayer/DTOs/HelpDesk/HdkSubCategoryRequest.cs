
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkSubCategoryRequest
    {
        [JsonIgnore]
        public int IdSubCategory { get; set; }
        public int IdCategory { get; set; }
        public string Descripcion { get; set; }
        public int CantidadHoraSLA { get; set; }
        public bool EsAsignacionDirecta { get; set; }
        public int IdDepartamento { get; set; }
        public string IdUsuarioAsignacion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
