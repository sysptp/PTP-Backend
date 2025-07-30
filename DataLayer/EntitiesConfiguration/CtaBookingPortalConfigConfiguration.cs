using DataLayer.Models.ModuloCitas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCitas
{
    public class CtaBookingPortalConfigConfiguration : IEntityTypeConfiguration<CtaBookingPortalConfig>
    {
        public void Configure(EntityTypeBuilder<CtaBookingPortalConfig> builder)
        {
            builder.ToTable("CtaBookingPortalConfig");

            // Clave primaria
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id");

            // Propiedades básicas
            builder.Property(e => e.CompanyId).HasColumnName("CompanyId").IsRequired();
            builder.Property(e => e.PortalName).HasColumnName("PortalName").IsRequired().HasMaxLength(100);
            builder.Property(e => e.Description).HasColumnName("Description").HasMaxLength(500);
            builder.Property(e => e.RequireAuthentication).HasColumnName("RequireAuthentication").HasDefaultValue(true);
            builder.Property(e => e.IsActive).HasColumnName("IsActive").HasDefaultValue(true);
            builder.Property(e => e.CustomSlug).HasColumnName("CustomSlug").HasMaxLength(50);
            builder.Property(e => e.DefaultReasonId).HasColumnName("DefaultReasonId");
            builder.Property(e => e.DefaultPlaceId).HasColumnName("DefaultPlaceId");
            builder.Property(e => e.DefaultStateId).HasColumnName("DefaultStateId");
            builder.Property(e => e.DefaultAppointmentDuration).HasColumnName("DefaultAppointmentDuration");
            builder.Property(e => e.AvailableDaysJson).HasColumnName("AvailableDaysJson").HasMaxLength(100);
            builder.Property(e => e.StartTime).HasColumnName("StartTime");
            builder.Property(e => e.EndTime).HasColumnName("EndTime");
            builder.Property(e => e.MaxAdvanceDays).HasColumnName("MaxAdvanceDays").HasDefaultValue(30);

            // Propiedades de auditoría
            builder.Property(e => e.FechaAdicion).HasColumnName("FechaAdicion").HasColumnType("DateTime");
            builder.Property(e => e.UsuarioAdicion).HasColumnName("UsuarioAdicion").HasMaxLength(50);
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion").HasColumnType("DateTime");
            builder.Property(e => e.UsuarioModificacion).HasColumnName("UsuarioModificacion").HasMaxLength(50);
            builder.Property(e => e.Borrado).HasColumnName("Borrado").HasDefaultValue(false);

            // Índices
            builder.HasIndex(e => e.CustomSlug)
                .IsUnique()
                .HasDatabaseName("IX_CtaBookingPortalConfig_CustomSlug_Unique")
                .HasFilter("[CustomSlug] IS NOT NULL AND [Borrado] = 0");

            builder.HasIndex(e => e.CompanyId)
                .HasDatabaseName("IX_CtaBookingPortalConfig_CompanyId");

            // Relaciones directas (Many-to-One)
            builder.HasOne(d => d.Company)
                .WithMany()
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_CtaBookingPortalConfig_GnEmpresa_CompanyId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.DefaultReason)
                .WithMany()
                .HasForeignKey(d => d.DefaultReasonId)
                .HasConstraintName("FK_CtaBookingPortalConfig_CtaAppointmentReason_DefaultReasonId")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(d => d.DefaultPlace)
                .WithMany()
                .HasForeignKey(d => d.DefaultPlaceId)
                .HasConstraintName("FK_CtaBookingPortalConfig_CtaMeetingPlace_DefaultPlaceId")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(d => d.DefaultState)
                .WithMany()
                .HasForeignKey(d => d.DefaultStateId)
                .HasConstraintName("FK_CtaBookingPortalConfig_CtaState_DefaultStateId")
                .OnDelete(DeleteBehavior.SetNull);

            // Configurar las relaciones Many-to-Many
            builder.HasMany(p => p.PortalUsers)
                .WithOne(pu => pu.Portal)
                .HasForeignKey(pu => pu.PortalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PortalAreas)
                .WithOne(pa => pa.Portal)
                .HasForeignKey(pa => pa.PortalId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}