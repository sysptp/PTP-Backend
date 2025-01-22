using DataLayer.Models.ModuloCampaña;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCampaña
{
    public class CmpCampanaConfiguration : IEntityTypeConfiguration<CmpCampana>
    {
        public void Configure(EntityTypeBuilder<CmpCampana> builder)
        {
            builder.ToTable("CmpCampana");
            builder.HasKey(x=> x.CampanaId);

            builder.HasOne(x => x.Estado)
                .WithMany(x => x.Campanas)
                .HasForeignKey(x => x.EstadoId);

            builder.HasOne(x => x.Plantilla)
                .WithMany(x => x.Campanas)
                .HasForeignKey(x => x.PlantillaId);
        }
    }
}
