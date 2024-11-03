using DataLayer.Models.Geografia;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessLayer.DTOs.Geografia.DMunicipio
{
    public class MunicipioResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ProvinceId { get; set; }
    }
}
