using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Auditoria
{
    public class AleAuditoria : AuditableEntities
    {
        [Key]
        public long IdAuditoria { get; set; }
        public string Modulo { get; set; }
        public string Acccion { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public int Hora { get; set; }
        public int Segundos { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string IP { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public string RolUsuario { get; set; }
        public long IdEmpresa { get; set; }
        public long IdSucursal { get; set; }
    }
}
