using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloGeneral.Archivos
{
    public class CreateGnUploadFileParametroDto
    {
        [Required]
        public long IdParametro { get; set; }
        public bool? EsExterno { get; set; }
        public int? IdTecnAlmExterno { get; set; }
    }
}
