using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.Otros
{
    public class AuditableEntitiesReponse
    {
        public DateTime FechaAdicion { get; set; } = DateTime.Now;
        public string UsuarioAdicion { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public bool Borrado { get; set; }
    }
}
