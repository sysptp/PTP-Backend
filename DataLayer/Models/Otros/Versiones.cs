using DataLayer.Models.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Otros
{
    public class Versiones : BaseModel
    {
        public Versiones()
        {
            FechaModificacion = DateTime.Now;
        }

        [StringLength(30),Required(ErrorMessage = "Nombre Requerido")]
        public string Nombre { get; set; }

        public bool Activo { get; set; } = true;

        [Required (ErrorMessage = "Marca Requerida")]
        public int IdMarca { get; set; }

        [ForeignKey("IdMarca")]
        public virtual Marca Marca { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
