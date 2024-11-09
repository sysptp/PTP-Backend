using DataLayer.Models.Geografia;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessLayer.DTOs.Configuracion.Geografia.DMunicipio
{
    public class MunicipioRequest
    {
        public string Name { get; set; } = null!;
        public int ProvinceId { get; set; }
    }
}
