using System.Text.Json.Serialization;

namespace BussinessLayer.DTOs.Auditoria
{
    public class AlePrintRequest
    {
        [JsonIgnore]
        public long IdPrint { get; set; }
        public string Reporte { get; set; }
        public string IdUsuario { get; set; }
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
