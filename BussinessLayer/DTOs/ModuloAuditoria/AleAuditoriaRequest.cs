using System.Text.Json.Serialization;


namespace BussinessLayer.DTOs.ModuloAuditoria
{
    public class AleAuditoriaRequest
    {
        [JsonIgnore]
        public long IdAuditoria { get; set; }
        public string Modulo { get; set; }
        public string Acccion { get; set; }

        public string Request { get; set; }
        public string Response { get; set; }
        public string IP { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public string RolUsuario { get; set; }
        public long IdEmpresa { get; set; }
        public long IdSucursal { get; set; }
        [JsonIgnore]
        public string? UserName { get; set; }
    }
}
