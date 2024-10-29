using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Reporteria
{
    [Table("VariablesReporteria")]
    public class VariablesReporteria
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string NombreVariable { get; set; }

        [MaxLength(20)]
        public string TipoVariable { get; set; }

        [MaxLength(1)]
        public string Estado { get; set; }

        public int? IdEmpresa { get; set; }

        public bool? EsObligatorio { get; set; }

        [MaxLength(50)]
        public string ValorPorDefecto { get; set; }

        [MaxLength(50)]
        public string Variable { get; set; }

        public int? IdCentroReporteria { get; set; }

        [ForeignKey("IdCentroReporteria")]
        public virtual CentroReporteria CentroReporteria { get; set; }
    }
}
