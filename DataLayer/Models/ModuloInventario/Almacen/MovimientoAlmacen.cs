using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("InvMovimientoAlmacen")]
public class MovimientoAlmacen 
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int IdSuplidor { get; set; }

    [Required]
    public int IdTipoMovimiento { get; set; }

    public long IdEmpresa { get; set; }

    [Required]
    public int IdAlmacen { get; set; }

    [Required]
    public int IdMetodoPago { get; set; }

    [Required]
    [MaxLength(100)]
    public string? NoFactura { get; set; }

    [MaxLength(100)]
    public string? Comprobante { get; set; }

    [Required]
    public int CantidadProducto { get; set; }

    [Required]
    public bool EsCredito { get; set; }

    public decimal? MontoInicial { get; set; }

    public decimal? MontoPendiente { get; set; }

    [Required]
    public long TotalFacturado { get; set; }

    [MaxLength(1500)]
    public string? Comentario { get; set; }

    [MaxLength(100)]
    public string? NoReferencia { get; set; }

    [MaxLength(100)]
    public string? NoAutorizacion { get; set; }

    [Required]
    public bool Borrado { get; set; }

    [Required]
    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    [MaxLength(50)]
    public string? UsuarioModificacion { get; set; }

    [Required]
    [MaxLength(50)]
    public string? UsuarioCreacion { get; set; }
}



