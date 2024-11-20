using DataLayer.Models.Otros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.HelpDesk
{
    public class HdkDepartXUsuario:AuditableEntities
    {
        [Key]
        public int IdDepartXUsuario { get; set; }
        public string IdUsuarioDepto { get; set; }
        public int IdDepartamento { get; set; }
        public long IdEmpresa { get; set; }
        
    }
}
