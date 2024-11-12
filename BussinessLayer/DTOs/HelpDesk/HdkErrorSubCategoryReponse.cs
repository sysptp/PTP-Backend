using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkErrorSubCategoryReponse: AuditableEntitiesReponse
    {
        public int IdErroSubCategory { get; set; }
        public int IdSubCategory { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
