using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("InvAlmacenInventario")]
public class InvAlmacenInventario
{
    [Key]
    public int Id { get; set; }

    public int? IdProducto { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdAlmacen { get; set; }

    public int? CantidadProducto { get; set; }

    public int? CantidadMinima { get; set; }

    [MaxLength(500)]
    public string? UbicacionExhibicion { get; set; }

    [MaxLength(500)]
    public string? UbicacionGuardada { get; set; }

    public bool? Activo { get; set; }

    public bool? Borrado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    [MaxLength(30)]
    public string? UsuarioCreacion { get; set; }

    [MaxLength(30)]
    public string? UsuarioModificacion { get; set; }
}
