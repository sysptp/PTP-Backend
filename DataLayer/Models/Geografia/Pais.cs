using DataLayer.Models.Otros;
using System;

namespace DataLayer.Models.Geografia
{
    public class Pais : BaseModel
    {
        public string Nombre { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
