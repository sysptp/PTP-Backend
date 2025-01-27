using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloHelpDesk
{
    public class HdkTicketsReponse : AuditableEntitiesReponse
    {
        public int IdTicket { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string IdUsuarioAsignado { get; set; } = null!;
        public int idTicketPadre { get; set; }
        public string? ReferenciasTicketExterno { get; set; } 
        public string? Solucion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int IdTipoTicket { get; set; }
        public string? TipoTicket { get; set; }
        public int IdDepartamentos { get; set; }
        public string? Departamento { get; set; }
        public int IdCategoria { get; set; }
        public string? Categoria { get; set; }
        public int IdSubCategoria { get; set; }
        public string? SubCategoria { get; set; }
        public int IdErrorCategoria { get; set; }
        public string? ErrorCategoria { get; set; } 
        public int IdEstado { get; set; }
        public string? Estado { get; set; }
        public int IdTipoSolucion { get; set; }
        public string? TipoSolucion { get; set; }
        public long IdEmpresa { get; set; }
        public string? NombreEmpresa { get; set; }
        public int IdPrioridad { get; set; }
        public string? Prioridad { get; set; }
    }
}
