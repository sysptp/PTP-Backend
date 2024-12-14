using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.ModuloAuditoria
{
    public class AleLogin : AuditableEntities
    {
        [Key]
        public long IdLogin { get; set; }
        public bool EsEntrada { get; set; }
        public DateTime? FechaEntrada { get; set; } = DateTime.Now;
        public DateTime? FechaSalida { get; set; }
        public string Usuario { get; set; }
        public int Ano { get; set; } = DateTime.Now.Year;
        public int Mes { get; set; } = DateTime.Now.Month;
        public int Dia { get; set; } = DateTime.Now.Day;
        public int Hora { get; set; } = DateTime.Now.Hour;
        public int Minutos { get; set; } = DateTime.Now.Minute;
        public int Segundos { get; set; } = DateTime.Now.Second;
        public string IP { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public string RolUsuario { get; set; }
        public string EstadoRol { get; set; }
        public long IdEmpresa { get; set; }
        public long IdSucursal { get; set; }
    }
}
