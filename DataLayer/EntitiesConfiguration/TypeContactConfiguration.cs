using DataLayer.Models.Contactos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration
{
    public class TypeContactConfiguration : IEntityTypeConfiguration<TypeContact>
    {
        public void Configure(EntityTypeBuilder<TypeContact> builder)
        {
            builder.ToTable("GNTIPOCONTACTOS");
            builder.HasKey(t => t.Id);
            builder.Property(t=> t.Id).IsRequired();
            builder.Property(t=> t.BussinesId).HasColumnName("IdEmpresa");
            builder.Property(t=> t.Format).HasColumnName("Formato");
            builder.Property(t=> t.Description).HasColumnName("Descripcion");
            builder.Property(t=> t.AddedBy).HasColumnName("UsuarioCreacion");
            builder.Property(t=> t.ModifiedBy).HasColumnName("UsuarioModificacion");
            builder.Property(t=> t.DateAdded).HasColumnName("FechaCreacion");
            builder.Property(t=> t.DateUpdated).HasColumnName("FechaModificacion");
            builder.Property(t=> t.IsDeleted).HasColumnName("Borrado");
            builder.Property(t=> t.IsActive).HasColumnName("Activo");

            builder.HasQueryFilter(x => !x.IsDeleted && x.IsActive);          
            

        }
    }
}
