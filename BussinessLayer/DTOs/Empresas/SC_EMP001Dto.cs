
using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.Empresas
{
    public class SC_EMP001Dto
    {
        public long CODIGO_EMP { get; set; }

        public string? NOMBRE_EMP { get; set; }

        public string? LOGO_EMP { get; set; }

        public string? RNC_EMP { get; set; }
        public string? DIRECCION { get; set; }
        public string? TELEFONO1 { get; set; }

        public string? TELEFONO2 { get; set; }

        public string? EXT_TEL1 { get; set; }

        public string? EXT_TEL2 { get; set; }
        public int CANT_SUCURSALES { get; set; }
        public int CANT_USUARIO { get; set; }
        public string? WEB { get; set; }
        public int USUARIO_ADICCION { get; set; }
        public DateTime FECHA_ADICION { get; set; }
        public int USUARIO_MODIFICACION { get; set; }
        public DateTime FECHA_MODIFICACION { get; set; }
    }
}
