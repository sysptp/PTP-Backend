using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloGeneral.Archivos
{
    public class ViewGnTecnoAlmacenExternoDto
    {
        public int Id { get; set; }
        public long IdEmpresa { get; set; }
        public string? Descripcion { get; set; }
        public string? UsuaridioExteno { get; set; }
        public string? PassWordExt { get; set; }
        public string? TokenExtert { get; set; }
        public bool? Estado { get; set; }
        public DateTime FechaAdicion { get; set; }
        public string? UsuarioAdicion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
