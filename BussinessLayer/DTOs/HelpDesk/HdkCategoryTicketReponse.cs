using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkCategoryTicketReponse: AuditableEntitiesReponse
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
    }
}
