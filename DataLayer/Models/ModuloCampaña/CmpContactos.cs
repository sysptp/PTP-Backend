using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.ModuloCampaña
{
    public class CmpContactos
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
        public bool Borrado { get; set; }
        public long EmpresaId{ get; set; }

        // Relaciones
        public virtual CmpTipoContacto TipoContacto { get; set; } = null!;
        public virtual CmpCliente Cliente { get; set; } = null!;

    }

}
