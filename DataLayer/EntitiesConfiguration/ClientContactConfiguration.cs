using DataLayer.Models.Contactos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration
{
    public class ClientContactConfiguration : IEntityTypeConfiguration<ClientContact>
    {
        public void Configure(EntityTypeBuilder<ClientContact> builder)
        {
            builder.ToTable("CLIENTECONTACTO");
            builder.HasKey(x => x.Id);
            builder.Property(t => t.ClientId).HasColumnName("IdCliente");
            builder.Property(t => t.TypeContactId).HasColumnName("IdTipoContacto");
            builder.Property(t => t.Value).HasColumnName("Valor");
            builder.Property(t => t.AddedBy).HasColumnName("IdUsuarioCreacion");
            builder.Property(t => t.ModifiedBy).HasColumnName("IdUsuarioModificacion");
            builder.Property(t => t.DateAdded).HasColumnName("FechaCreacion");
            builder.Property(t => t.DateUpdated).HasColumnName("FechaModificacion");
            builder.Property(t => t.IsDeleted).HasColumnName("Borrado");
            builder.Property(t => t.IsActive).HasColumnName("Activo");

            //Relacion con tabla clientes.
            builder.HasOne(x => x.Client)
                .WithMany(x => x.ClientContacts)
                .HasForeignKey(x => x.ClientId);
            
            //Relacion con tabla tipo de contactos. 
            builder.HasOne(x => x.TypeContact)
                .WithMany(x => x.ClientContacts)
                .HasForeignKey(x => x.TypeContactId);

        }
    }
}
