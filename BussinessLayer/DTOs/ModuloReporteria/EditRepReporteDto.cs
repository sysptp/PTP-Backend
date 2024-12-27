using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloReporteria
{
    public class EditRepReporteDto
    {
        public int Id { get; set; }
        public string? NombreReporte { get; set; }
        public bool? EsPesado { get; set; }
        public bool? EsSubquery { get; set; }
        public string? DescripcionReporte { get; set; }
        public string? QueryCommand { get; set; }
        public bool? Activo { get; set; }
    }
}
