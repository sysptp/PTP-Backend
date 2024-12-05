using DataLayer.Models.Otros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.ModuloHelpDesk
{
    public class HdkNoteTicket : AuditableEntities
    {
        [Key]
        public int IdNota { get; set; }
        public string Notas { get; set; }
        public int IdTicket { get; set; }
        public long IdEmpresa { get; set; }


    }
}
