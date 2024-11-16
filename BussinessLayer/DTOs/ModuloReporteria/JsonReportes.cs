using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.CentroReporteriaDTOs
{
    public class JsonReportes
    {
        public int Id { get; set; }
        public int NumQuery { get; set; }
        public string NombreReporte { get; set; }
        public string DescripcionReporte { get; set; }
        public bool EsPesado { get; set; }
        public bool EsSubquery { get; set; }
        public string QueryCommand { get; set; }
    }
}
