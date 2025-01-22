using DataLayer.Models.Otros;

namespace DataLayer.Models.ModuloCampaña
{
    public class CmpLogsEnvio : AuditableEntities

        
    {
        public CmpLogsEnvio()
        {
            
        }
        //Constructor para cuando sea con una plantilla de CmpPlantillas.
        public CmpLogsEnvio(int plantillaId, string asunto, string destinatarios, string? respuesta, long empresaId)
        {
            PlantillaId = plantillaId;
            Respuesta = respuesta;
            EmpresaId = empresaId;
            Asunto = asunto;
            Destinatarios = destinatarios;
        }
        //Constructor para cuando sea un mensaje sin tener que ser plantilla.
        public CmpLogsEnvio(string? contenido, string asunto, string destinatarios,string? respuesta, long empresaId)
        {
            Contenido = contenido;
            Respuesta = respuesta;
            EmpresaId = empresaId;
            Asunto = asunto;
            Destinatarios = destinatarios;
            
        }

        public int LogId { get; set; }
        public int PlantillaId { get; set; }
        public string? Contenido { get; set; }
        public string? Asunto { get; set; }
        public string? Destinatarios { get; set; }
        public string? Respuesta { get; set; }
        public long EmpresaId { get; set; }


    }
}
