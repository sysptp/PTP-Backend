using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkSubCategoryReponse : AuditableEntitiesReponse
    {
        public int IdSubCategory { get; set; }
        public int IdCategory { get; set; }
        public string Descripcion { get; set; } = null!;
        public int CantidadHoraSLA { get; set; }
        public bool EsAsignacionDirecta { get; set; }
        public int IdDepartamento { get; set; }
        public HdkDepartamentsReponse? HdkDepartamentsReponse { get; set; }
        public string IdUsuarioAsignacion { get; set; } = null!;
        public string NombreUsuario {  get; set; } = null!;
        public long IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; } = null!;
    }
      
}
