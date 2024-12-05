using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.ModuloReporteria
{
    public class RepReporte
    {
        public int Id { get; set; }
        public int? IdEmpresa { get; set; }
        public int? NumQuery { get; set; }
        public string? NombreReporte { get; set; }
        public bool? EsPesado { get; set; }
        public bool? EsSubquery { get; set; }
        public string? DescripcionReporte { get; set; }
        public string? QueryCommand { get; set; }
        public bool? Activo { get; set; }
        public bool? Borrado { get; set; }
        public DateTime? FechaAdicion { get; set; }
        public string? UsuarioAdicion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Navigation Property
        public virtual ICollection<RepReportesVariable> RepReportesVariables { get; set; } = new List<RepReportesVariable>();
    }
}
