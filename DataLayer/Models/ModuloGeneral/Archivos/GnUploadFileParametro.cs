using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.ModuloGeneral.Archivos
{
    public class GnUploadFileParametro
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public long IdParametro { get; set; } 

        public bool? EsExterno { get; set; } 

        public int? IdTecnAlmExterno { get; set; } 

        public bool Borrado { get; set; }
    }
}
