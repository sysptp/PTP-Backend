using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.ModuloGeneral.Archivos
{
    public class GnTecnoAlmacenExterno
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public long IdEmpresa { get; set; } 

        [MaxLength(150)]
        public string? Descripcion { get; set; } 

        [MaxLength(50)]
        public string? UsuaridioExteno { get; set; } 

        [MaxLength(200)]
        public string? PassWordExt { get; set; } 

        public string? TokenExtert { get; set; } 

        public bool? Estado { get; set; } 

        [Required, MaxLength(50)]
        public string UsuarioAdicion { get; set; } 

        [Required]
        public DateTime FechaAdicion { get; set; } 

        public DateTime? FechaModificacion { get; set; } 

        [MaxLength(50)]
        public string? UsuarioModificacion { get; set; } 

        public bool? Borrado { get; set; } 
    }
}
