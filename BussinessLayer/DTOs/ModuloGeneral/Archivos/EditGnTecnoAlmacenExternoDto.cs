using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloGeneral.Archivos
{
    public class EditGnTecnoAlmacenExternoDto
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(150)]
        public string? Descripcion { get; set; }

        [MaxLength(50)]
        public string? UsuaridioExteno { get; set; }

        [MaxLength(200)]
        public string? PassWordExt { get; set; }

        public string? TokenExtert { get; set; }

        public bool? Estado { get; set; }
    }
}
