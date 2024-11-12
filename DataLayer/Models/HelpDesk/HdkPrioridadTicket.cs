using DataLayer.Models.Otros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.HelpDesk
{
    public class HdkPrioridadTicket:AuditableEntities
    {
        [Key]
        public int IdPrioridad { get; set; }
        public string Descripcion { get; set; }
        public long IdEmpresa { get; set; }
        
    }
}
