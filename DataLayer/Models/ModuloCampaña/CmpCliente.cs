using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.ModuloCampaña
{
    public class CmpCliente
    {
        public int ClientId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = string.Empty;
        public string UsuarioModificacion { get; set; } = string.Empty;
        public bool Borrado { get; set; }
        public long EmpresaId { get; set; }
        public virtual ICollection<CmpContactos> Contactos { get; set; } = new HashSet<CmpContactos>();
        public virtual ICollection<CmpCampanaDetalle> CampanaDetalles { get; set; } = new HashSet<CmpCampanaDetalle>();

    }
}
