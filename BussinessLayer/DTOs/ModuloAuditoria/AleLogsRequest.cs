using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.ModuloAuditoria
{
    public class AleLogsRequest
    {
        [JsonIgnore]
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
        public string IP { get; set; }
        public decimal Latitud { get; set; }
        public decimal Logitud { get; set; }
        public long IdEmpresa { get; set; }
        public long IdSucursal { get; set; }
    }
}
