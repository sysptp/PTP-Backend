using BussinessLayer.DTOs.CentroReporteriaDTOs;
using System.Collections.Generic;

namespace BussinessLayer.DTOs.CentroReporteria
{
    public class EjecutarReporteDTOs
    {
        public string QueryCommand { get; set; }
        public List<JsonVariables> Variables { get; set; }
    }
}
