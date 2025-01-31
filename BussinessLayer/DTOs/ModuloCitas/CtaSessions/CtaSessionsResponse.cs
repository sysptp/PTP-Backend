
using DataLayer.Models.ModuloGeneral;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessLayer.DTOs.ModuloCitas.CtaSessions
{
    public class CtaSessionsResponse
    {
        public int IdSession { get; set; }
        public string? Description { get; set; }
        public int? IdUser { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public DateTime FirstSessionDate { get; set; }
        public DateTime LastSessionDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        public int CompletedAppointments { get; set; }
        public int RepeatEvery { get; set; }
        public int RepeatUnitId { get; set; }
        public string RepeatUnitDescription { get; set; } = null!;
        public long CompanyId { get; set; }
    }
}
