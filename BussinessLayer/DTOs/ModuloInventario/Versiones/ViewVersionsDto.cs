using DataLayer.Models.ModuloInventario.Marcas;
using DataLayer.Models.ModuloInventario.Productos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Versiones
{
    public class ViewVersionsDto
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public bool? Activo { get; set; }

        public int? IdMarca { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public bool? Borrado { get; set; }

        public long? IdEmpresa { get; set; }

        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public virtual Marca? Marca { get; set; }

        public virtual ICollection<Producto>? Productos { get; set; }
    }
}
