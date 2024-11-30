
using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.HelpDesk
{
    public class HdkTicketsRequest
    {
        [JsonIgnore]
        public int IdTicket { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string IdUsuarioAsignado { get; set; }
        public int idTicketPadre { get; set; }
        public string ReferenciasTicketExterno { get; set; }
        public string Solucion { get; set; }
        public DateTime? FechaVencimiento { get; set; } 
        public DateTime? FechaCierre { get; set; } 
        public int IdTipoTicket { get; set; }
        public int IdDepartamentos { get; set; }
        public int IdCategoria { get; set; }
        public int IdSubCategoria { get; set; }
        public int IdErrorCategoria { get; set; }
        public int IdEstado { get; set; }
        public int IdTipoSolucion { get; set; }
        public long IdEmpresa { get; set; }
        public int IdPrioridad { get; set; }
    }
}
