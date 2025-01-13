namespace DataLayer.Models.ModuloCampaña
{
    public class CmpTipoContacto
    {
        public int TipoContactoId { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } = string.Empty;
        public string UsuarioModificacion { get; set; } = string.Empty;
        public bool Borrado { get; set; }
        public long EmpresaId { get; set; }
        public virtual ICollection<CmpContactos> Contactos { get; set; } = new HashSet<CmpContactos>();

    }
}
