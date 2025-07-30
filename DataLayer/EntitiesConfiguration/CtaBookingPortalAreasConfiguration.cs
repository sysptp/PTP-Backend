using DataLayer.Models.ModuloCitas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCitas
{
    public class CtaBookingPortalAreasConfiguration : IEntityTypeConfiguration<CtaBookingPortalAreas>
    {
        public void Configure(EntityTypeBuilder<CtaBookingPortalAreas> builder)
        {
            builder.ToTable("CtaBookingPortalAreas");

            // Clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id");

            // Propiedades
            builder.Property(e => e.PortalId).HasColumnName("PortalId").IsRequired();
            builder.Property(e => e.AreaId).HasColumnName("AreaId").IsRequired();
            builder.Property(e => e.IsDefault).HasColumnName("IsDefault").HasDefaultValue(false);

            // Propiedades de auditoría
            builder.Property(e => e.FechaAdicion).HasColumnName("FechaAdicion").HasColumnType("DateTime");
            builder.Property(e => e.UsuarioAdicion).HasColumnName("UsuarioAdicion").HasMaxLength(50);
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion").HasColumnType("DateTime");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioModificacion").HasMaxLength(50);
            builder.Property(e => e.Borrado).HasColumnName("Borrado").HasDefaultValue(false);

            // Índices
            builder.HasIndex(e => new { e.PortalId, e.AreaId })
                .IsUnique()
                .HasDatabaseName("IX_CtaBookingPortalAreas_Portal_Area_Unique")
                .HasFilter("[Borrado] = 0");

            builder.HasIndex(e => e.PortalId)
                .HasDatabaseName("IX_CtaBookingPortalAreas_PortalId");

            builder.HasIndex(e => e.AreaId)
                .HasDatabaseName("IX_CtaBookingPortalAreas_AreaId");

            // Relaciones
            builder.HasOne(d => d.Portal)
                .WithMany(p => p.PortalAreas)
                .HasForeignKey(d => d.PortalId)
                .HasConstraintName("FK_CtaBookingPortalAreas_CtaBookingPortalConfig_PortalId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.Area)
                .WithMany()
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK_CtaBookingPortalAreas_CtaAppointmentArea_AreaId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}