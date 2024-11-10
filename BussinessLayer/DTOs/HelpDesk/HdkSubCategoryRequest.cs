
namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkSubCategoryRequest
    {
        public int IdCategory { get; set; }
        public string Descripcion { get; set; }
        public int CantidadHoraSLA { get; set; }
        public bool EsAsignacionDirecta { get; set; }
        public int IdDepartamento { get; set; }
        public int IdUsuarioAsignacion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
