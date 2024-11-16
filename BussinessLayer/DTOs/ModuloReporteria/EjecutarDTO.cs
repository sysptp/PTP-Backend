using System.Collections.Generic;
using BussinessLayer.DTOs.CentroReporteriaDTOs;

namespace BussinessLayer.DTOs.CentroReporteria
{
    public class EjecutarDTO
    {
        public DataLayer.Models.Reporteria.CentroReporteria Reporte {  get; set; }

        public List<JsonVariables> Variables { get; set; }
    }
}
