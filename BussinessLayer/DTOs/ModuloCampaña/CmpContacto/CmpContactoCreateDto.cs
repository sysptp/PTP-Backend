using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloCampaña.CmpContacto
{
    public class CmpContactoCreateDto
    {
        public int ClienteId { get; set; }
        public string Contacto { get; set; } = string.Empty;
        public int TipoContactoId { get; set; }
        public bool Estado { get; set; }
        public long EmpresaId { get; set; }
        public string UsuarioCreacion { get; set; } = string.Empty;
    }
}
