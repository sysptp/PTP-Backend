using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Otros
{
    public  class TipoPago : BaseModel
    {
        [StringLength(30)]
        public string Name { get; set; }
        public string IN_OUT { get; set; }
    }
}
