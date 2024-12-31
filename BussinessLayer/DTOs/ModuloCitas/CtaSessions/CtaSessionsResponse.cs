using DataLayer.Models.ModuloGeneral.Seguridad;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.ModuloCitas.CtaSessions
{
    public class CtaSessionsResponse
    {
        public int IdSession { get; set; }
        public string? Description { get; set; }
        public string? IdClient { get; set; }
        public string? ClientName { get; set; }
        public string? ClientEmail { get; set; }
        public string? ClientPhoneNumber { get; set; }
        public string? IdUser { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public DateTime FirstSessionDate { get; set; }
        public int? IdReason { get; set; }
        public int IdState { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
