using DataLayer.Models.Otros;
using System.ComponentModel.DataAnnotations;


namespace DataLayer.Models.ModuloGeneral
{
   public class GnParametrosGenerales: AuditableEntities 
    {
        [Key]
        public long IdParametro { get; set; }
        public string VariableParametro { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }
        public int IdModulo { get; set; }      
        public long IdEmpresa { get; set; }
        
    }
}
