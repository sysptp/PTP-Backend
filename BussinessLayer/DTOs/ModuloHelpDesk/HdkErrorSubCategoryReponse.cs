using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkErrorSubCategoryReponse : AuditableEntitiesReponse
    {
        public int IdErroSubCategory { get; set; }
        public int IdSubCategory { get; set; }
        public HdkSubCategoryReponse? HdkSubCategoryReponse { get; set; }
        public string Descripcion { get; set; } = null!;
        public long IdEmpresa { get; set; }
        public string? NombreEmpresa { get; set; }
    }
}
