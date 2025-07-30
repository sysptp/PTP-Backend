using DataLayer.Models.ModuloCitas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCitas
{
    public class CtaBookingPortalUsersConfiguration : IEntityTypeConfiguration<CtaBookingPortalUsers>
    {
        public void Configure(EntityTypeBuilder<CtaBookingPortalUsers> builder)
        {
            builder.ToTable("CtaBookingPortalUsers");

            // Clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id");

            // Propiedades
            builder.Property(e => e.PortalId).HasColumnName("PortalId").IsRequired();
            builder.Property(e => e.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(e => e.IsMainAssignee).HasColumnName("IsMainAssignee").HasDefaultValue(false);

            // Propiedades de auditoría
            builder.Property(e => e.FechaAdicion).HasColumnName("FechaAdicion").HasColumnType("DateTime");
            builder.Property(e => e.UsuarioAdicion).HasColumnName("UsuarioAdicion").HasMaxLength(50);
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion").HasColumnType("DateTime");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioModificacion").HasMaxLength(50);
            builder.Property(e => e.Borrado).HasColumnName("Borrado").HasDefaultValue(false);

            // Índices
            builder.HasIndex(e => new { e.PortalId, e.UserId })
                .IsUnique()
                .HasDatabaseName("IX_CtaBookingPortalUsers_Portal_User_Unique")
                .HasFilter("[Borrado] = 0");

            builder.HasIndex(e => e.PortalId)
                .HasDatabaseName("IX_CtaBookingPortalUsers_PortalId");

            builder.HasIndex(e => e.UserId)
                .HasDatabaseName("IX_CtaBookingPortalUsers_UserId");

            // Relaciones
            builder.HasOne(d => d.Portal)
                .WithMany(p => p.PortalUsers)
                .HasForeignKey(d => d.PortalId)
                .HasConstraintName("FK_CtaBookingPortalUsers_CtaBookingPortalConfig_PortalId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_CtaBookingPortalUsers_Usuario_UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}