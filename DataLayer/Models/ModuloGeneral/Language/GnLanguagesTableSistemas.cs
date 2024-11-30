
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloGeneral.Language
{
    public class GnLanguagesTableSistemas
    {
        [Key]
        public string CodigoUnico { get; set; } = null!;
        public string SchemaName { get; set; } = null!;
        public string TableViewName { get; set; } = null!;
        public string CodeColumnName { get; set; } = null!;

    }
}
