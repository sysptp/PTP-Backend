public class ViewBrandDto
{
    public int Id { get; set; }

    public long? IdEmpresa { get; set; }

    public string? Nombre { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public bool? Borrado { get; set; }

    public virtual ICollection<Versiones>? Versiones { get; set; }
}

