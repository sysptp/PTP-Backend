using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Otros
{
    public  class DgiiNcfSecuencia 
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(3)]
        public string SerieTipoComprobante { get; set; }

        public int Secuecial { get; set; }

        public int IdDgiiNcf   { get; set; }
        [ForeignKey("IdDgiiNcf")]
        public virtual DgiiNcf DgiiNcf { get; set; }

        public bool Estado { get; set; } //true = usado(No se puede modificar si no fue enviado a la DGII), false 

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaModificacion { get; set; }


        public bool Borrado { get; set; }

        public DateTime FechaCreacion { get; set; }

    }
}
