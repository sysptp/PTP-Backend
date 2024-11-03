using DataLayer.Models.Otros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.Precios
{
    public class CreatePreciosDto : BaseModel
    {
        public int ProductoId { get; set; }
        public decimal Valor { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Borrado { get; set; }
    }
}
