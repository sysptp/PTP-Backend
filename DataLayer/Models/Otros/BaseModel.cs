using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Otros
{
    public class BaseModel
    {
        public BaseModel()
        {
            FechaCreacion =  DateTime.Now;
            Borrado = false;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Borrado { get; set; } = false;

        public long IdEmpresa { get; set; }
    }
}
