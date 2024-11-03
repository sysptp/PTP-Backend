using DataLayer.Models.Geografia;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessLayer.DTOs.Geografia.DProvincia
{
    public class ProvinceRequest
    {
        public string Name { get; set; } = null!;
        public int RegionId { get; set; }
    }
}
