using DataLayer.Models.ModuloInventario.Otros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.ModuloInventario.Suplidores
{
    public class ViewSuppliersDto
    {
        public int? Id { get; set; }

        public long? IdEmpresa { get; set; }

        public int? TipoIdentificacion { get; set; }

        public string? NumeroIdentificacion { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public string? TelefonoPrincipal { get; set; }

        public string? DireccionPrincipal { get; set; }

        public string? Email { get; set; }

        public string? PaginaWeb { get; set; }

        public string? Descripcion { get; set; }

        public bool? Borrado { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public virtual ICollection<InvProductoSuplidor>? ProductoSuplidores { get; set; }

        public virtual ICollection<Pedido>? Pedidos { get; set; }
    }
}
