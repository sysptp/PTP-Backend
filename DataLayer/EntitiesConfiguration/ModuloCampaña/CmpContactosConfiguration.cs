using DataLayer.Models.ModuloCampaña;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CmpContactosConfiguration : IEntityTypeConfiguration<CmpContactos>
{
    public void Configure(EntityTypeBuilder<CmpContactos> builder)
    {
        builder.HasKey(c => c.ContactoId);

        builder.Property(c => c.Contacto)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.TipoContactoId)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.UsuarioCreacion)
               .HasMaxLength(50);

        builder.Property(c => c.UsuarioModificacion)
               .HasMaxLength(50);

      

        builder.HasOne(c => c.TipoContacto)
              .WithMany(t => t.Contactos)
              .HasForeignKey(c => c.TipoContactoId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Cliente)
               .WithMany(cl => cl.Contactos)
               .HasForeignKey(c => c.ClienteId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
