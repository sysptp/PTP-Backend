
using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.Auditoria
{
    public class AlePrint : AuditableEntities
    {
        [Key]
        public long IdPrint { get; set; }
        public string Reporte { get; set; }
        public DateTime FechaPint { get; set; }
        public string RolUsuario { get; set; }
        public string EstadoUsuario { get; set; }
        public string IP { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public long IdEmpresa { get; set; }
        public long IdSucursal { get; set; }
    }
}
