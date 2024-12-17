using BussinessLayer.DTOs.Otros;

namespace BussinessLayer.DTOs.ModuloAuditoria
{
    public class AleBitacoraReponse : AuditableEntitiesReponse
    {
        public long IdBitacora { get; set; }
        public string Modulo { get; set; } = null!;
        public string Acccion { get; set; } = null!;
        public int Ano { get; set; }
        public int Mes { get; set; }
        public int Minutos { get; set; }
        public int Dia { get; set; }
        public int Hora { get; set; }
        public int Segundos { get; set; }
        public string? Request { get; set; }
        public string? Response { get; set; }
        public string? IP { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public string RolUsuario { get; set; } = null!;
        public long IdEmpresa { get; set; }
        public long IdSucursal { get; set; }
    }
}
