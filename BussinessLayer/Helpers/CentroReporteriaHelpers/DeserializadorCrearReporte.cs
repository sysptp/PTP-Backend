using BussinessLayer.DTOs.CentroReporteriaDTOs;
using Newtonsoft.Json;

namespace BussinessLayer.Helpers.CentroReporteriaHelpers
{
    public class DeserializadorCrearReporte
    {
        public DeserializarCreateReporte DeserializarCreate(string jsonVariable, string jsonReportes)
        {
            // Deserializar el JSON en un objeto ReporteModel
            JsonReportes reporte = JsonConvert.DeserializeObject<JsonReportes>(jsonReportes);

            List<JsonVariables> variables = JsonConvert.DeserializeObject<List<JsonVariables>>(jsonVariable);
         
            return new DeserializarCreateReporte { JsonReportes = reporte, JsonVariables = variables };
        }
    }
}
