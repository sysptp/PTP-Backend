using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.ModuloReporteria
{
    public class RepReportesVariable
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int IdCentroReporteria { get; set; }
        public string? NombreVariable { get; set; }
        public string? TipoVariable { get; set; }
        public bool? EsObligatorio { get; set; }
        public string? ValorPorDefecto { get; set; }
        public string? Variable { get; set; }
        public bool Borrado { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaAdicion { get; set; }
        public string? UsuarioAdicion { get; set; }

        // Navigation Property
        public virtual RepReporte? RepReporte { get; set; }
    }
}
