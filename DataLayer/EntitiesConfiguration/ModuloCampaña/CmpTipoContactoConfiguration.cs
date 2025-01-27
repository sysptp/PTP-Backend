using DataLayer.Models.ModuloCampaña;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCampaña
{
    public class CmpTipoContactoConfiguration : IEntityTypeConfiguration<CmpTipoContacto>
    {
        public void Configure(EntityTypeBuilder<CmpTipoContacto> builder)
        {

            builder.ToTable("CmpTipoContacto");
            builder.HasKey(tc => tc.TipoContactoId); 
            builder.Property(tc => tc.Descripcion)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(tc => tc.UsuarioCreacion)
                   .HasMaxLength(50);
            builder.Property(tc => tc.UsuarioModificacion)
                   .HasMaxLength(50);

            
        }
    }
}
