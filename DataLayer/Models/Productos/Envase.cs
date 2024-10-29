using DataLayer.Models.Otros;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Productos
{
    public class Envase : BaseModel
    {
        public Envase()
        {
            FechaModificacion = DateTime.Now;
            Activo = true;
        }
        [Required(ErrorMessage = "Descripcion Requerida")]
        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaModificacion { get; set; }

    }
}
