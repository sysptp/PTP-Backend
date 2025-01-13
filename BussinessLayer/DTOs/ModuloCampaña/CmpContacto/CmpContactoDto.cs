using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloCampaña.CmpContacto
{
    public class CmpContactoDto
    {
        public int ContactoId { get; set; }
        public int ClienteId { get; set; }
        public string Contacto { get; set; } = string.Empty;
        public int TipoContactoId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = string.Empty;
        public string UsuarioModificacion { get; set; } = string.Empty;
        public long EmpresaId { get; set; }
    }
}
