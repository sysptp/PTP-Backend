using DataLayer.Models.ModuloInventario.Pedidos;

public class ViewOrderDto
{
    public int Id { get; set; }

    public long? IdEmpresa { get; set; }

    public int? IdSuplidor { get; set; }

    public bool? Solicitado { get; set; }

    public bool? Borrado { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Suplidores? Suplidor { get; set; }

    public virtual List<DetallePedido>? Detalle { get; set; }
}

