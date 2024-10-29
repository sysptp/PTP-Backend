using DataLayer.Models.Otros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Productos
{
    public class Marca:BaseModel
    {
        [StringLength(30),Required(ErrorMessage = "Nombre Requerido")]
        public string Nombre  { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaModificacion { get; set; } = DateTime.Now;

        public  virtual ICollection<Versiones> Versiones { get; set; }

    }
}
