using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Reporteria
{
    [Table("CentroReporteria")]
    public class CentroReporteria
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string NombreReporte { get; set; }

        public int? IdEmpresa { get; set; }

        [MaxLength(1)]
        public string Estado { get; set; }

        public bool? EsPesado { get; set; }

        public bool? EsSubquery { get; set; }

        [MaxLength(200)]
        public string DescripcionReporte { get; set; }

        public DateTime? FechaAdicion { get; set; }

        [MaxLength(30)]
        public string AdicionadoPor { get; set; }

        public int? NumQuery { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string QueryCommand { get; set; }

        // Relación uno a muchos
        public virtual ICollection<VariablesReporteria> VariablesReporterias { get; set; }
    }
}
