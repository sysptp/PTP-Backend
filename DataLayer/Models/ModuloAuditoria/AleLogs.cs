using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.ModuloAuditoria
{
    public class AleLogs : AuditableEntities
    {
        [Key]
        public long IdLogs { get; set; }
        public string Logs { get; set; }
        public string Modulo { get; set; }
        public string Accion { get; set; }
        public string LogDescripcion { get; set; }
        public bool EsLogDb { get; set; }
        public string UrlSend { get; set; }
        public string FuncionMentodo { get; set; }
        public string IdUsuario { get; set; }
        public DateTime FechaAdicion { get; set; }
        public string RolUsuario { get; set; }
        public string EstadoRol { get; set; }
        public int Ano { get; set; } = DateTime.Now.Year;
        public int Mes { get; set; } = DateTime.Now.Month;
        public int Dia { get; set; } = DateTime.Now.Day;
        public int Hora { get; set; } = DateTime.Now.Hour;
        public int Minutos { get; set; } = DateTime.Now.Minute;
        public int Segundos { get; set; } = DateTime.Now.Second;
        public string IP { get; set; }
        public decimal Latitud { get; set; }
        public decimal Logitud { get; set; }
        public long IdEmpresa { get; set; }
        public long IdSucursal { get; set; }
    }
}
