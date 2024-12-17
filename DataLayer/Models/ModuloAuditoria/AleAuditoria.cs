using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.ModuloAuditoria
{
    [Table("AleBitacora")]
    public class AleAuditoria : AuditableEntities
    {
        [Key]
        public long IdBitacora { get; set; }
        public string Modulo { get; set; }
        public string Acccion { get; set; }
        public int Ano { get; set; } = DateTime.Now.Year;
        public int Mes { get; set; } = DateTime.Now.Month;
        public int Dia { get; set; } = DateTime.Now.Day;
        public int Hora { get; set; } = DateTime.Now.Hour;
        public int Minutos { get; set; } = DateTime.Now.Minute;
        public int Segundos { get; set; } = DateTime.Now.Second;
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
