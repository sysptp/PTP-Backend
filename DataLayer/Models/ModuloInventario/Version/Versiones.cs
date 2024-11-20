using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Models.ModuloInventario.Marcas;
using DataLayer.Models.ModuloInventario.Productos;

[Table("InvVersiones")]
public class Versiones
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string? Nombre { get; set; }

    [Required]
    public bool? Activo { get; set; }

    [Required]
    public int? IdMarca { get; set; }

    [Required]
    public DateTime? FechaModificacion { get; set; }

    [Required]
    public DateTime? FechaCreacion { get; set; }

    [Required]
    public bool? Borrado { get; set; }

    public long? IdEmpresa { get; set; }

    [Required]
    public string? UsuarioCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    // Relación con Marca (muchos a uno)
    [ForeignKey("IdMarca")]
    public virtual InvMarcas? Marca { get; set; }

    // Relación con Producto (uno a muchos)
    public virtual ICollection<Producto>? Productos { get; set; }
}

