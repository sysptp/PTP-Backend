using DataLayer.Models.ModuloInventario.Productos;
using DataLayer.Models.ModuloGeneral.Monedas;

public class ViewPreciosDto
{
    public int? Id { get; set; }

    public int? IdProducto { get; set; }

    public long? IdEmpresa { get; set; }

    public int? IdMoneda { get; set; }

    public decimal PrecioValor { get; set; }

    public bool? HabilitarVenta { get; set; }

    public bool? Borrado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Producto? Producto { get; set; }

    public virtual Moneda? Moneda { get; set; }
}

