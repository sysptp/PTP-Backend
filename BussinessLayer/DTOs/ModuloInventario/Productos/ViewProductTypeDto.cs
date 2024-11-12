using DataLayer.Models.ModuloInventario.Productos;

namespace BussinessLayer.DTOs.ModuloInventario.Productos
{
    public class ViewProductTypeDto
    {
        public int? Id { get; set; }

        public int? IdEmpresa { get; set; }

        public string? NombreTipoProducto { get; set; }

        public string? UsuarioCreacion { get; set; }

        public string? UsuarioModificacion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public bool? Borrado { get; set; }

        public bool? Activo { get; set; }

        public virtual ICollection<Producto>? Productos { get; set; }
    }
}
