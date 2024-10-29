using DataLayer.Models.Geografia;
using DataLayer.Models.Otros;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Suplidor
{
    public class Suplidores : BaseModel
    {
        [StringLength(60), Required]
        public string Nombre { get; set; }

        public int Rnc { get; set; }

        [StringLength(20), Required]
        public string Telefono { get; set; }

        [StringLength(100), Required]
        public string Direccion { get; set; }

        [StringLength(40)]
        public string Email { get; set; }

        public int IdMunicipio { get; set; }

        [ForeignKey("IdMunicipio")]
        public virtual Municipio Municipio { get; set; }

        public string Imagen { get; set; }

        public string Web { get; set; }

        public string Contacto1 { get; set; }

        public string TelefonoC1 { get; set; }

        public string ExtC1 { get; set; }

        public string Contacto2 { get; set; }

        public string TelefonoC2 { get; set; }

        public string ExtC2 { get; set; }

        public string Contacto3 { get; set; }

        public string TelefonoC3 { get; set; }

        public string ExtC3 { get; set; }

        public string ComentarioC1 { get; set; }

        public string ComentarioC2 { get; set; }

        public string ComentarioC3 { get; set; }

        public DateTime Fecha_Modificacion { get; set; }

        public int Usuario_Adicion { get; set; }

        public int Usuario_Modificacion { get; set; }
    }
}
