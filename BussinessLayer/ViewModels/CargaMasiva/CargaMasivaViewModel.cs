using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web;

namespace BussinessLayer.ViewModels
{
    public class CargaMasivaViewModel
    {
        public List<string> TableNames { get; set; }
        public string SeletedTable { get; set; }
        public bool ShowBottons { get; set; }

        [MaxLength(2, ErrorMessage = "El delimitador debe tener un máximo de 2 caracteres.")]
        [RegularExpression(@"^[\W_]+$", ErrorMessage = "El delimitador debe ser un carácter especial.")]
        public string Delimitador { get; set; }
        public IFormFile File { get; set; }
    }

}
