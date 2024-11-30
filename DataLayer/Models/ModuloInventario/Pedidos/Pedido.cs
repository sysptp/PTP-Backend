using DataLayer.Models.ModuloInventario.Pedidos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("InvPedido")]
public class Pedido
{
    [Key]
    public int Id { get; set; }

    [Required]
    public long? IdEmpresa { get; set; }

    [Required]
    public int? IdSuplidor { get; set; }

    [Required]
    public bool? Solicitado { get; set; }

    [Required]
    public bool? Borrado { get; set; }

    [Required]
    public bool? Activo { get; set; }

    public DateTime? FechaModificacion { get; set; }

    [Required]
    public DateTime? FechaCreacion { get; set; }

    [Required]
    [MaxLength(15)]
    public string? UsuarioCreacion { get; set; }

    [MaxLength(15)]
    public string? UsuarioModificacion { get; set; }

    // Navigation Property
    [ForeignKey("IdSuplidor")]
    public virtual Suplidores? Suplidor { get; set; }

    public virtual List<DetallePedido>? Detalle { get; set; }
    public virtual ICollection<DetallePedido>? DetallePedidos { get; set; }
}
