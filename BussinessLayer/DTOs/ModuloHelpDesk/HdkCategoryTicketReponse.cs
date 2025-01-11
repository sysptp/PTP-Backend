using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkCategoryTicketReponse : AuditableEntitiesReponse
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; } = null!;
        public long IdEmpresa { get; set; }
        public string? NombreEmpresa { get; set; }
    }
}
