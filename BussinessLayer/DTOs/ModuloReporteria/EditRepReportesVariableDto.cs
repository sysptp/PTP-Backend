using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloReporteria
{
    public class EditRepReportesVariableDto
    {
        public int Id { get; set; }
        public string NombreVariable { get; set; } 
        public string TipoVariable { get; set; } 
        public bool EsObligatorio { get; set; }
        public string? ValorPorDefecto { get; set; }
        public string Variable { get; set; }
    }
}
