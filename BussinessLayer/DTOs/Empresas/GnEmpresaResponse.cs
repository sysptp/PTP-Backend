
namespace BussinessLayer.DTOs.Empresas
{
    public class GnEmpresaDto
    {
        public long CODIGO_EMP { get; set; }
        public string NOMBRE_EMP { get; set; } = null!;
        public string? LOGO_EMP { get; set; }
        public string RNC_EMP { get; set; } = null!;
        public string DIRECCION { get; set; } = null!;
        public string TELEFONO1 { get; set; } = null!;
        public string? TELEFONO2 { get; set; }
        public string? EXT_TEL1 { get; set; }
        public string? EXT_TEL2 { get; set; }
        public int CANT_SUCURSALES { get; set; }
        public int CANT_USUARIO { get; set; }
        public string? WEB { get; set; }
    }
}
