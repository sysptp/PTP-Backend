using DataLayer.Models.ModuloCampaña;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.EntitiesConfiguration.ModuloCampaña
{
    public class CmpPlantillasConfiguration : IEntityTypeConfiguration<CmpPlantillas>
    {
        public void Configure(EntityTypeBuilder<CmpPlantillas> builder)
        {
            builder.ToTable("CmpPlantillas");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.CmpTipoPlantilla)
                .WithMany(x => x.Plantillas)
                .HasForeignKey(x => x.TipoPlantillaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
