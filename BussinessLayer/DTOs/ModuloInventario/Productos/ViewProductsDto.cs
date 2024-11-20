using DataLayer.Models.ModuloInventario.Productos;

public class ViewProductsDto
{
    public int? Id { get; set; }

    public long? IdEmpresa { get; set; }

    public int? IdVersion { get; set; }

    public virtual Versiones? Version { get; set; }

    public int? IdTipoProducto { get; set; }

    public virtual InvTipoProducto? TipoProducto { get; set; }

    public string? CodigoBarra { get; set; }

    public string? Codigo { get; set; }

    public string? NombreProducto { get; set; }

    public string? Descripcion { get; set; }

    public int? CantidadLote { get; set; }

    public int? CantidadInventario { get; set; }

    public int? CantidadMinima { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool? AdmiteDescuento { get; set; }

    public bool? HabilitaVenta { get; set; }

    public bool? AplicaImpuesto { get; set; }

    public bool? EsLote { get; set; }

    public bool? EsProducto { get; set; }

    public bool? EsLocal { get; set; }

    public bool? Borrado { get; set; }

    public bool? Activo { get; set; }

    public string? UsuarioCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<InvProductoImpuesto>? InvProductoImpuestos { get; set; }

    public virtual ICollection<InvProductoSuplidor>? InvProductoSuplidores { get; set; }

    public virtual ICollection<InvProductoImagen> InvProductoImagenes { get; set; } = [];
}

