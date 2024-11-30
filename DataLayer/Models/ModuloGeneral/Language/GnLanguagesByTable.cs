
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloGeneral.Language
{
    public class GnLanguagesByTable
    {
        [Key]
        public string CodigoLanguagesByTable { get; set; } = null!;
        public string CodigoUnico { get; set; } = null!;
        public string LanguageCode { get; set; } = null!;
        public string ColumnName { get; set; } = null!;

    }
}
