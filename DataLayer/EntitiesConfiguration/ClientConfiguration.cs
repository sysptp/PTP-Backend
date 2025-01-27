using DataLayer.Models.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clientes");
            
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.CodeBussines).HasColumnName("IdEmpresa");
            builder.Property(x => x.CodeTypeIdentification).HasColumnName("TipoIdentificacion");
            builder.Property(x => x.Identification).HasColumnName("NumeroIdentificacion");
            builder.Property(x => x.Name).HasColumnName("Nombres");
            builder.Property(x => x.LastName).HasColumnName("Apellidos");
            builder.Property(x => x.Phone).HasColumnName("TelefonoPrincipal");
            builder.Property(x => x.Address).HasColumnName("DireccionPrincipal");
            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.WebSite).HasColumnName("PaginaWeb");
            builder.Property(x => x.Description).HasColumnName("Descripcion");
            builder.Property(x => x.IsDeleted).HasColumnName("Borrado");
            builder.Property(x => x.AddedBy).HasColumnName("UsuarioAdicion");
            builder.Property(x => x.ModifiedBy).HasColumnName("UsuarioModificacion");
            builder.Property(x => x.DateAdded).HasColumnName("FechaAdicion").HasColumnType("DateTime");
            builder.Property(x => x.DateModified).HasColumnName("FechaModificacion").HasColumnType("DateTime");
        }
    }
}
