using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.Auditoria
{
    public class AleLoginRequest
    {
        [JsonIgnore]
        public long IdLogin { get; set; }
        public bool EsEntrada { get; set; }
        public DateTime? FechaEntrada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string Usuario { get; set; }
        public string IP { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public string RolUsuario { get; set; }
        public string EstadoRol { get; set; }
        public long IdEmpresa { get; set; }
        public long IdSucursal { get; set; }
    }
}
