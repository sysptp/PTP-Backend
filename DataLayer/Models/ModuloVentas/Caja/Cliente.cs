using DataLayer.Models.ModuloGeneral.Geografia;
using DataLayer.Models.Otros;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    public class Clientes : BaseModel
    {
        [StringLength(40), Required]
        public string Nombre { get; set; }

        [StringLength(40)]
        public string Apellido { get; set; }

        public DateTime? Fecha_Cumplano { get; set; }


        [StringLength(20)]
        public string Cedula { get; set; }

        [StringLength(20)]
        public string Rnc { get; set; }

        [StringLength(100)]
        public string Empresa { get; set; }

        [StringLength(100)]
        public string Direccion { get; set; }

        [StringLength(100)]
        public string Referencia { get; set; }

        public string Imagen { get; set; }

        [StringLength(70)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Telefono1 { get; set; }

        [StringLength(20)]
        public string Telefono2 { get; set; }

        public int IdMunicipio { get; set; }

        [ForeignKey("IdMunicipio")]
        public virtual Municipio Municipio { get; set; }

        public DateTime Fecha_Modificacion { get; set; }

        public int Usuario_Adicion { get; set; }

        public int Usuario_Modificacion { get; set; }

        public string Sexo { get; set; }

        public int Cod_Tipo_Identificacion { get; set; }

        [NotMapped]
        public Tipo_Identificacion Tipo_Identificacion { get; set; }

        public int Cod_Ciudad { get; set; }

        public int Cod_Pais_Nacionalidad { get; set; }

        public int Cod_Empresa { get; set; }

        public int Cod_Sucursal { get; set; }

        public int Cod_Pais { get; set; }

        public int Cod_Region { get; set; }

        public int Cod_Provincia { get; set; }
    }

