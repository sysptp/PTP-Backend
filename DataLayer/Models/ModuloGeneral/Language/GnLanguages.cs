
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.ModuloGeneral.Language
{
    public class GnLanguages
    {
        [Key] 
        public long IdLanguages { get; set; }
        public string LanguageCode { get; set; } = null!;
        public string LanguageName { get; set; } = null!;
        public string NativeName { get; set; } = null!;
        public bool IsDefault { get; set; }

    }
}
