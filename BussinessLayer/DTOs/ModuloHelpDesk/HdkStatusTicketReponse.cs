using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkStatusTicketReponse : AuditableEntitiesReponse
    {
        public int IdEstado { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool EsCierre { get; set; }
        public long IdEmpresa { get; set; }
        public string? NombreEmpresa { get; set; }
        public int IdDepartamento { get; set; }
        public HdkDepartamentsReponse? HdkDepartamentsReponse { get; set; }
        public int OrdenStatus { get; set; }
    }
}
